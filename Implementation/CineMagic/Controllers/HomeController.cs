using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CineMagic.Models;
using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Repositories;

namespace CineMagic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMoviesRepository _moviesRepository;

        public HomeController(ILogger<HomeController> logger, IMoviesRepository moviesRepository)
        {
            _logger = logger;
            this._moviesRepository = moviesRepository;
        }

        public async Task<IActionResult> Index()
        {
            IList<MovieGetDetailsRes> movies = await _moviesRepository.GetAllMoviesAsync();
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
    }
}
