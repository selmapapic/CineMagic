using CineMagic.Facade.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IReservationsRepository
    {
        Task<IList<ReservationGetDetailsRes>> GetUserReservationsAsync(ReservationGetDetailsByUserIdReq req);
    }
}
