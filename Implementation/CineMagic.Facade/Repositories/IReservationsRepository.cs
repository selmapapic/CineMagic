using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.Ticket;
using CineMagic.Facade.Models.TicketModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IReservationsRepository
    {
        Task<UserReservationModel> GetUserReservationsAsync(ReservationGetDetailsByUserIdReq req);
        Task<TicketGetDetailsRes> GetTicketForIdAsync(TicketGetDetailsReq req);
        Task CancelReservation(CancelReservationModel model);
    }
}
