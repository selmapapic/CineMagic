using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CineMagic.Models;
using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.PlayingNow;

namespace CineMagic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;

        public HomeController(ILogger<HomeController> logger, IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository)
        {
            _logger = logger;
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
        }

        public async Task<IActionResult> Index()
        {
            IList<MovieRes> movies = await _moviesRepository.GetAllMoviesAsync();
            var sorted = movies.OrderByDescending(id => id.Id).ToList();
            foreach(var m in sorted)
            {
                Console.WriteLine(m.Name);
            }
            return View(sorted);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> PlayingNow()
        {
            PlayingNowGetDetailsRes projections = await _projectionsRepository.GetAllProjectionsByDaysAsync();
            return View(projections);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Administrator()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }
    }
}
