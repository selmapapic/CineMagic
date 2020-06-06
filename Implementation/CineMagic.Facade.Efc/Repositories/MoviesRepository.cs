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
using CineMagic.Facade.Models.Projection;

namespace CineMagic.Facade.Efc.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private CineMagicDbContext _dbContext;
        private IProjectionsRespository _projectionsRepository;

        public MoviesRepository(CineMagicDbContext dbContext, IProjectionsRespository projectionsRespository)
        {
            
            this._dbContext = dbContext;
            this._projectionsRepository = projectionsRespository;
        }

        public async Task<MovieRes> GetDetailsAsync(MovieGetDetailsReq req)
        {

            ProjectionGetDetailsByMovieIdReq projectionReq = new ProjectionGetDetailsByMovieIdReq
            {
                MovieId = req.Id 
            };
            IList<ProjectionRes> projections = await _projectionsRepository.GetProjectionsForMovieAsync(projectionReq);

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
                    Projections = projections,

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

        

        public async Task<Boolean> DeleteMovie(int id)
        {
            Movie movie = await _dbContext.Movies.Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            try
            {

                _dbContext.Movies.Remove(movie);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        
        }

        public async Task<Movie> GetMovieById(MovieGetDetailsReq req)
        {
            Movie movie = await _dbContext.Movies.Where(m => m.Id == req.Id)
                .FirstOrDefaultAsync();
            return movie;
        }
    }
}
