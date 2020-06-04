using CineMagic.Dal.Context;
using CineMagic.Facade.Models.CinemaCreditCard;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class CinemaCreditCardRepository : ICinemaCreditCardRepository
    {
        private CineMagicDbContext _dbContext;

        public CinemaCreditCardRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<CinemaCreditCardGetDetailsRes> GetCreditCard(CinemaCreditCardGetDetailsReq req)
        {
            CinemaCreditCardGetDetailsRes res = await _dbContext.CinemaCreditCards
                .Where(c => c.UserId == req.UserId)
                .Select(c => new CinemaCreditCardGetDetailsRes
                {
                    Id = c.Id,
                    CardNumber = c.CinemaCreditCardNumber,
                    Balance = c.Balance
                }).FirstOrDefaultAsync();

            return res;
        }
    }
}
