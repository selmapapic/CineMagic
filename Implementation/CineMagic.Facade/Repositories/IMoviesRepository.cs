using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Repositories
{
    public interface IMoviesRepository
    {
        Task<MovieGetDetailsRes> GetDetailsAsync(MovieGetDetailsReq req);
        Task<IList<MovieGetDetailsRes>> GetAllMoviesAsync();
    }
}
