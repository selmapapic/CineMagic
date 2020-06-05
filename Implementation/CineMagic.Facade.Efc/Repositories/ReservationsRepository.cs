using CineMagic.Dal.Context;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private CineMagicDbContext _dbContext;

        public ReservationsRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IList<ReservationGetDetailsRes>> GetUserReservationsAsync(ReservationGetDetailsByUserIdReq req)
        {
            IList<ReservationGetDetailsRes> reservations = await _dbContext.Reservations.
                Where(m => m.UserId == req.UserId)
                .Select(m => new ReservationGetDetailsRes
                {
                    //UserTicket = m.Ticket
                }).ToListAsync();
                
            return reservations;
        }
    }
}
