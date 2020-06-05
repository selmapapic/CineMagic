using CineMagic.Dal.Context;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class AvailableSeatsRepository : IAvailableSeatsRepository
    {
        private CineMagicDbContext _dbContext;

        public AvailableSeatsRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<AvailableSeatGetDetailsRes> GetAvailableSeatDetails(AvailableSeatGetDetailsReq req)
        {
            AvailableSeatGetDetailsRes res = await _dbContext.AvailableSeats
                .Where(avs => avs.Id == req.Id)
                .Select(avs => new AvailableSeatGetDetailsRes
                {
                    Id = req.Id,
                    SeatId = avs.SeatId,
                    ProjectionId = avs.ProjectionId,
                    Name = avs.Seat.Name
                }).FirstOrDefaultAsync();

            return res;
        }
    }
}
