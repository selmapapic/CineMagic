using CineMagic.Dal.Context;
using CineMagic.Facade.Models.CinemaHall;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class CinemaHallRepository : ICinemaHallRepository
    {

        private CineMagicDbContext _dbContext;

        public CinemaHallRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<CinemaHallGetDetailsRes> GetCinemaHallDetails(CinemaHallGetDetailsReq req)
        {
            CinemaHallGetDetailsRes res = await _dbContext.CinemaHalls
                .Where(c => c.Id == req.Id)
                .Select(c => new CinemaHallGetDetailsRes
                {
                    Id = c.Id,
                    HallImageUrl = c.HallImageUrl,
                    numberOfSeats = c.AllSeats.Count()
                }).FirstOrDefaultAsync();

            return res;
        }
    }
}
