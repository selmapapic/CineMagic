using CineMagic.Dal.Context;
using CineMaigc.Facade.Models.Movie;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineMagic.Facade.Efc.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private CineMagicDbContext _dbContext;
        public MoviesRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<MovieGetDetailsRes> GetDetailsAsync(MovieGetDetailsReq req)
        {
            MovieGetDetailsRes res = await _dbContext.Movies
                .Where(m => m.Id == req.Id)
                .Select(m => new MovieGetDetailsRes
                {
                    Name = m.Name,
                    TrailerURL = m.TrailerURL,
                    Duration = m.Duration,
                    Synopsis = m.Synopsis,
                    Director = m.Director,

                    GenreNames = m.GenreLinks
                        .Select(mgl => mgl.MovieGenre.Name)
                        .ToList(),

                    ActorNames = m.ActorMovieLinks
                        .Select(aml => aml.Actor.Name)
                        .ToList()
                }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<IList<MovieGetDetailsRes>> GetAllMoviesAsync()
        {

            IList<MovieGetDetailsRes> res = await _dbContext.Movies
                .Select(m => new MovieGetDetailsRes
                {
                    Name = m.Name,
                    TrailerURL = m.TrailerURL,
                    Duration = m.Duration,
                    Synopsis = m.Synopsis,
                    Director = m.Director,

                    GenreNames = m.GenreLinks
                        .Select(mgl => mgl.MovieGenre.Name)
                        .ToList(),

                    ActorNames = m.ActorMovieLinks
                        .Select(aml => aml.Actor.Name)
                        .ToList()
                }).ToListAsync();

            return res;
        }
    }
}
