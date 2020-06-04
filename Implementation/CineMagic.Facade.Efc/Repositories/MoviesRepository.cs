using CineMagic.Dal.Context;
using CineMagic.Facade.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineMagic.Facade.Models.Movie;
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

        public async Task<MovieGetDetailsRes> GetDetailsAsync(MovieGetDetailsReq req)
        {


            ProjectionGetDetailsByMovieIdReq projectionReq = new ProjectionGetDetailsByMovieIdReq
            {
                MovieId = req.Id 
            };
            IList<ProjectionGetDetailsRes> projections = await _projectionsRepository.GetProjectionsForMovieAsync(projectionReq);

            MovieGetDetailsRes res = await _dbContext.Movies
                .Where(m => m.Id == req.Id)
                .Select(m => new MovieGetDetailsRes
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

        public async Task<IList<MovieGetDetailsRes>> GetAllMoviesAsync()
        {

            IList<MovieGetDetailsRes> res = await _dbContext.Movies
                .Select(m => new MovieGetDetailsRes
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
    }
}
