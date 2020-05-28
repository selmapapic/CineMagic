using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineMagic.Controllers
{
    public class MoviesController : Controller
    {
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRespository;

        public MoviesController(IMoviesRepository moviesRepository, IProjectionsRespository projectionsRespository)
        {
            this._moviesRepository = moviesRepository;
            this._projectionsRespository = projectionsRespository;
        }

        public async Task<IActionResult> Details(MovieGetDetailsReq req)
        {
            MovieGetDetailsRes res = await _moviesRepository.GetDetailsAsync(req);
            return View(res);
        }

        public async Task<IActionResult> AllMovies()
        {
            IList<MovieGetDetailsRes> movies = await _moviesRepository.GetAllMoviesAsync();

            return View(movies);
        }

        
        public async Task<IActionResult> MovieReservation(MovieGetDetailsReq req)
        {
            MovieGetDetailsRes movieRes = await _moviesRepository.GetDetailsAsync(req);
            ProjectionGetDetailsReq projectionReq = new ProjectionGetDetailsReq
            {
                MovieId = movieRes.Id
            };

            IList<ProjectionGetDetailsRes> projectionsRes = await _projectionsRespository.GetProjectionsForMovieAsync(projectionReq);

            return View(projectionsRes);
        }


    }
}
