using CineMagic.Dal.Context;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.Ticket;
using CineMagic.Facade.Models.TicketModel;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using CineMagic.Dal.Entities;

namespace CineMagic.Facade.Efc.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private CineMagicDbContext _dbContext;
        private IProjectionsRespository _projectionsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ReservationsRepository(CineMagicDbContext dbContext, IProjectionsRespository projectionsRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._projectionsRepository = projectionsRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserReservationModel> GetUserReservationsAsync(ReservationGetDetailsByUserIdReq req)
        {
            IList<TicketGetDetailsRes> reservationsRes = new List<TicketGetDetailsRes>();
            IList<ReservationIdForUserRes> reservations = await _dbContext.Reservations.
                Where(m => m.UserId == req.UserId)
                .Select(m => new ReservationIdForUserRes
                {
                    TicketId = m.TicketId
                }).ToListAsync();

            foreach(var item in reservations)
            {
                TicketGetDetailsReq ticketGetDetailsReq = new TicketGetDetailsReq
                {
                    Id = item.TicketId
                };
                TicketGetDetailsRes ticketGetDetailsRes = await this.GetTicketForIdAsync(ticketGetDetailsReq);
                reservationsRes.Add(ticketGetDetailsRes);
            }
            return new UserReservationModel
            {
                Tickets = reservationsRes
            };
        }

        public async Task<TicketGetDetailsRes> GetTicketForIdAsync (TicketGetDetailsReq req)
        {
            TicketGetDetailsRes res = await _dbContext.Tickets
                .Where(t => t.Id == req.Id)
                .Select(t => new TicketGetDetailsRes
                {
                    Id = t.Id,
                    Price = t.Price, 
                    ProjectionId = t.ProjectionId, 
                    SeatName = t.Seat.Name,
                    SeatId = t.SeatId
                }).FirstOrDefaultAsync();

            ProjectionGetDetailsReq projectionReq = new ProjectionGetDetailsReq
            {
                Id = res.ProjectionId
            };

            ProjectionRes projectionRes = await _projectionsRepository.GetProjectionById(projectionReq);

            res.Projection = projectionRes;

            return res;
        }

        public async Task CancelReservation(CancelReservationModel model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Reservation reservation = await _dbContext.Reservations
                .Where(rs => rs.UserId == userId && rs.TicketId == model.TicketId)
                .FirstOrDefaultAsync();

            Ticket ticket = await _dbContext.Tickets
                .Where(t => t.Id == model.TicketId)
                .FirstOrDefaultAsync();

            AvailableSeat availableSeat = new AvailableSeat
            {
                ProjectionId = model.ProjectionId,
                SeatId = model.SeatId
            };

            CinemaCreditCard card = await _dbContext.CinemaCreditCards
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();

            card.Balance = card.Balance + 7;

            _dbContext.Reservations.Remove(reservation);
            _dbContext.Tickets.Remove(ticket);
            _dbContext.AvailableSeats.Add(availableSeat);
            await _dbContext.SaveChangesAsync();
        }
    }
}
