using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Models.User;
using CineMagic.Facade.Models.Ticket;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CineMagic.Facade.Models.TicketModel;

namespace CineMagic.Controllers
{
    public class ReservationsController : Controller
    {
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;
        private IAvailableSeatsRepository _availableSeatsRepository;
        private IReservationsRepository _reservationsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public ReservationsController(IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository, IAvailableSeatsRepository availableSeatsRepository, IReservationsRepository reservationsRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
            this._availableSeatsRepository = availableSeatsRepository;
            this._reservationsRepository = reservationsRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._userRepository = userRepository;
        }

        public async Task<IActionResult> MakeReservation(AvailableSeatGetDetailsReq req)
        {
            AvailableSeatGetDetailsRes availableSeatGetDetailsRes = await _availableSeatsRepository.GetAvailableSeatDetails(req);
            return View(availableSeatGetDetailsRes);
        }

        public async Task<IActionResult> TicketPayment(AvailableSeatGetDetailsReq req)
        {
            AvailableSeatGetDetailsRes availableSeatGetDetailsRes = await _availableSeatsRepository.GetAvailableSeatDetails(req);

            ProjectionGetDetailsReq projectionGetDetailsReq = new ProjectionGetDetailsReq
            {
                Id = availableSeatGetDetailsRes.ProjectionId
            };
            ProjectionRes projectionGetDetailsRes = await _projectionsRepository.GetProjectionById(projectionGetDetailsReq);

            ReservationGetDetailsRes reservation = new ReservationGetDetailsRes
            {
                Seat = availableSeatGetDetailsRes,
                Projection = projectionGetDetailsRes
            };
            return View(reservation); 
        }

        public async Task<IActionResult> CheckCardNumber(CheckReservationModel model)
        {
            UserGetDetailsRes user = await _userRepository.GetCurrentUser();
            String error1 = "Sorry, You don't have enough credits on your CineMagic Credit Card to buy this ticket.";
            String error2 = "You entered wrong credit card number, click on a button and try again.";

            long cardNumber = Convert.ToInt64(model.cardNumber);
            if (user.CinemaCreditCard == null || cardNumber != user.CinemaCreditCard.CardNumber)
            {
                model.Error = error2;
                return View("Unsuccessfully", model);
            }
            else if (user.CinemaCreditCard.Balance < 7.0)
            {
                model.Error = error1;
                return View("Unsuccessfully", model);
            }
            else
            {
                await _userRepository.CreateReservationAsync(model);
                return View("Successfully");
            }


        }

        public async Task<IActionResult> AllReservationsUserAsync ()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);   //uzimam id trenutno logovanog usera

            ReservationGetDetailsByUserIdReq reservationReq = new ReservationGetDetailsByUserIdReq
            {
                UserId = userId
            };

            UserReservationModel Model = await _reservationsRepository.GetUserReservationsAsync(reservationReq);

            return View(Model);
        }

        public async Task<IActionResult> ViewReservation(TicketGetDetailsReq req)
        {
            TicketGetDetailsRes res = await _reservationsRepository.GetTicketForIdAsync(req);
            return View(res);

        }

        public async Task<IActionResult> CancelReservation(CancelReservationModel model)
        {
            await _reservationsRepository.CancelReservation(model);
            return View("CancelReservation");
        }
    }
}
