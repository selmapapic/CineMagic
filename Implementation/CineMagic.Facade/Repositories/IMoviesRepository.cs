using CineMagic.Facade.Models.Movie;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IMoviesRepository
    {
        Task<MovieGetDetailsRes> GetDetailsAsync(MovieGetDetailsReq req);
        Task<IList<MovieGetDetailsRes>> GetAllMoviesAsync();
    }
}
