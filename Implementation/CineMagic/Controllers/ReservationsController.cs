using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CineMagic.Controllers
{
    public class ReservationsController : Controller
    {
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;
        private IAvailableSeatsRepository _availableSeatsRepository;
        private IReservationsRepository _reservationsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationsController(IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository, IAvailableSeatsRepository availableSeatsRepository, IReservationsRepository reservationsRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
            this._availableSeatsRepository = availableSeatsRepository;
            this._reservationsRepository = reservationsRepository;
            this._httpContextAccessor = httpContextAccessor;
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

        public async Task<IActionResult> AllReservationsUserAsync ()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);   //uzimam id trenutno logovanog usera

            ReservationGetDetailsByUserIdReq reservationReq = new ReservationGetDetailsByUserIdReq
            {
                UserId = userId
            };

            IList<ReservationGetDetailsRes> userReservationsRes = await _reservationsRepository.GetUserReservationsAsync(reservationReq);

            return View(userReservationsRes);
        }
    }
}
