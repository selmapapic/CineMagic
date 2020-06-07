using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.Movie;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IMoviesRepository
    {
        Task<MovieRes> GetDetailsAsync(MovieGetDetailsReq req);
        Task<IList<MovieRes>> GetAllMoviesAsync();
       
        
        public Task<Movie> GetMovieById(MovieGetDetailsReq req);
        Task<Boolean> AddMovies(MovieRes movie);
        Task<Boolean> DeleteMovies(int id);
        Task<IList<GenreRes>> GetMovieGenres();
    }
}
