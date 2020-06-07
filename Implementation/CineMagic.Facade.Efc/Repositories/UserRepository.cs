using CineMagic.Dal.Context;
using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.CinemaCreditCard;
using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Models.Ticket;
using CineMagic.Facade.Models.User;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CineMagicDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ICinemaCreditCardRepository _cinemaCreditCardRepository;
        private IAvailableSeatsRepository _availableSeatsRepository;


        public UserRepository(CineMagicDbContext dbContext, IHttpContextAccessor httpContextAccessor, ICinemaCreditCardRepository cinemaCreditCardRepository, IAvailableSeatsRepository availableSeatsRepository)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
            this._cinemaCreditCardRepository = cinemaCreditCardRepository;
            this._availableSeatsRepository = availableSeatsRepository;

        }

        public async Task<Boolean> DoesUserExists()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await DoesUserExists(userId);

        }
        private async Task<Boolean> DoesUserExists(string userId)
        {
            string id = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (id == null) return false;
            return true;
        }

        public async Task<UserGetDetailsRes> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IdentityUser user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            CinemaCreditCardGetDetailsReq req = new CinemaCreditCardGetDetailsReq
            {
                UserId = userId
            };

            CinemaCreditCardGetDetailsRes creditCard = await _cinemaCreditCardRepository.GetCreditCard(req);

            UserGetDetailsRes userDetails = new UserGetDetailsRes
            {
                User = user,
                CinemaCreditCard = creditCard
            };

            return userDetails;
        }

        public async Task CreateReservationAsync (CheckReservationModel model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await DoesUserExists(userId))
            {
                AvailableSeat availableSeat = await _dbContext.AvailableSeats
                    .Where(avs => avs.Id == model.AvailableSeatId)
                    .FirstOrDefaultAsync();

                Ticket ticket = new Ticket
                {
                    ProjectionId = model.ProjectionId,
                    SeatId = availableSeat.SeatId,
                    Price = 7.0
                };
                int seatId = availableSeat.SeatId;
                

                _dbContext.Tickets.Add(ticket);
                _dbContext.AvailableSeats.Remove(availableSeat);
                await _dbContext.SaveChangesAsync();
                await AddReservation(model, seatId);

            }
           
        }

        private async Task AddReservation (CheckReservationModel model, int seatId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            CinemaCreditCard cinemaCreditCard = await _dbContext.CinemaCreditCards
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            double balance = cinemaCreditCard.Balance;

            cinemaCreditCard.Balance = balance - 7.0;


            Ticket ticket = await _dbContext.Tickets
                .Where(t => t.ProjectionId == model.ProjectionId && t.SeatId == seatId)
                .FirstOrDefaultAsync();

            Reservation reservation = new Reservation
            {
                TicketId = ticket.Id,
                UserId = userId
            };

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCreditCard(CreditCardModel res)
        {
            string userId = await _dbContext.Users
                .Where(u => u.Email == res.UserMail)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            CinemaCreditCard card = new CinemaCreditCard
            {
                UserId = userId,
                Balance = res.Balance,
                CinemaCreditCardNumber = GetCardNumber()
            };

            _dbContext.Add(card);
            await _dbContext.SaveChangesAsync();
        }

        private long GetCardNumber ()
        {
            int _min = 1000000;
            int _max = 9999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public async Task AddFunds(AddFundsModel res)
        {
            CinemaCreditCard card = await _dbContext.CinemaCreditCards
                .Where(u => u.CinemaCreditCardNumber == res.CardNumber)
                .FirstOrDefaultAsync();

            card.Balance = card.Balance + res.Balance;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<CinemaCreditCardGetDetailsRes> GetUserCreditCard(CreditCardModel res)
        {
            IdentityUser user = await _dbContext.Users
                .Where(u => u.Email == res.UserMail)
                .FirstOrDefaultAsync();

            CinemaCreditCard card = await _dbContext.CinemaCreditCards
                .Where(u => u.UserId == user.Id)
                .FirstOrDefaultAsync();

            return new CinemaCreditCardGetDetailsRes
            {
                CardNumber = card.CinemaCreditCardNumber
            };
        }
    }
}
