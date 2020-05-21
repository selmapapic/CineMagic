using CineMagic.Facade.Models.Movie;
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

        public MoviesController(IMoviesRepository moviesRepository)
        {
            this._moviesRepository = moviesRepository;
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
    }
}
