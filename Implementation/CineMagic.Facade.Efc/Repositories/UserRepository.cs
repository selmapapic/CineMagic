using CineMagic.Dal.Context;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CineMagicDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserRepository(CineMagicDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<Boolean> DoesUserExists()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await DoesUserExists(userId);

        }
        private async Task<Boolean> DoesUserExists(string userId)
        {
            string id = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            if (id == null) return false;
            return true;
        }
    }
}
