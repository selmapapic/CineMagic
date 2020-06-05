using CineMagic.Facade.Efc.Repositories;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Models.User;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineMagic.Controllers
{
    public class ReservationsController : Controller
    {
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;
        private IAvailableSeatsRepository _availableSeatsRepository;
        private IUserRepository _userRepository;

        public ReservationsController(IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository, IAvailableSeatsRepository availableSeatsRepository, IUserRepository userRepository)
        {
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
            this._availableSeatsRepository = availableSeatsRepository;
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
            ProjectionGetDetailsRes projectionGetDetailsRes = await _projectionsRepository.GetProjectionById(projectionGetDetailsReq);

            ReservationGetDetailsRes reservation = new ReservationGetDetailsRes
            {
                Seat = availableSeatGetDetailsRes,
                Projection = projectionGetDetailsRes
            };
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> CheckCardNumber(CheckReservationModel model)
        {
            long number = Convert.ToInt64(model.cardNumber);

            UserGetDetailsRes user = await _userRepository.GetCurrentUser();

            if(user.CinemaCreditCard.CardNumber == number && user.CinemaCreditCard.Balance >= 7.0)
            {
                await _userRepository.CreateReservationAsync(model);
                return View("Successfully");
            }
            else
            {
                return View("Unsuccessfully", model);
            }
            
        }
    }
}
