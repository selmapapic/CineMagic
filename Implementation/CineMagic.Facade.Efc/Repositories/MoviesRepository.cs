using CineMagic.Dal.Context;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineMagic.Facade.Models.Movie;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CineMagic.Dal.Entities;

namespace CineMagic.Facade.Efc.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private CineMagicDbContext _dbContext;
        public MoviesRepository(CineMagicDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<MovieRes> GetDetailsAsync(MovieGetDetailsReq req)
        {

            IList<DateTime> dateTimes = await _dbContext.Projections
                .Where(p => p.MovieId == req.Id)
                .Select(p => p.ProjectionTime)
                .ToListAsync();

            MovieRes res = await _dbContext.Movies
                .Where(m => m.Id == req.Id)
                .Select(m => new MovieRes
                {
                    Id = m.Id,
                    Name = m.Name,
                    TrailerURL = m.TrailerUrl,
                    PosterURL = m.PosterUrl,
                    Duration = m.Duration,
                    Synopsis = m.Synopsis,
                    Director = m.Director,
                    ProjectionsDateTime = dateTimes,

                    GenreNames = m.GenreLinks
                        .Select(mgl => mgl.MovieGenre.Name)
                        .ToList(),

                    ActorNames = m.ActorMovieLinks
                        .Select(aml => aml.Actor.Name)
                        .ToList()

                }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<IList<MovieRes>> GetAllMoviesAsync()
        {

            IList<MovieRes> res = await _dbContext.Movies
                .Select(m => new MovieRes
                {
                    Id = m.Id,
                    Name = m.Name,
                    TrailerURL = m.TrailerUrl,
                    PosterURL = m.PosterUrl,
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
        public async Task<Boolean> AddMovie(Movie movie) 
        {
            try
            {
                _dbContext.Add(movie);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Boolean> DeleteMovie(MovieRes movie)
        {
            try
            {
                _dbContext.Remove(movie);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        
        }
        

    }
}
