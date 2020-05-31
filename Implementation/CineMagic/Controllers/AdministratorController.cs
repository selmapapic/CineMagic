using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineMagic.Facade.Models.Administrator;
using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CineMagic.Controllers
{
    public class AdministratorController : Controller
    {

        private readonly ILogger<AdministratorController> _logger;
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;

        public AdministratorController(ILogger<AdministratorController> logger, IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository)
        {
            _logger = logger;
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
        }


        public async Task<IActionResult> IndexMovies()
        {
            IList<MovieGetDetailsRes> movies = await _moviesRepository.GetAllMoviesAsync();
            var sorted = movies.OrderByDescending(id => id.Id).ToList();
            foreach (var m in sorted)
            {
                Console.WriteLine(m.Name);
            }
            return View(sorted);

        }
        public async Task<IActionResult> IndexProjections()
        {
            IList<ProjectionGetDetailsRes> projections = await _projectionsRepository.GetAllProjectionsAsync();
            var sorted = projections.OrderByDescending(time => time.ProjectionTime).ToList();
            foreach (var m in sorted)
            {
                Console.WriteLine(m.MovieName);
                Console.WriteLine(m.ProjectionTime);
            }
            return View(sorted);

        }
        public async Task<IActionResult> HomeAdmin()
        {
            IList<ProjectionGetDetailsRes> projections = await _projectionsRepository.GetAllProjectionsAsync();
            var sortedPro = projections.OrderByDescending(time => time.ProjectionTime).ToList();
            foreach (var m in sortedPro)
            {
                Console.WriteLine(m.MovieName);
                Console.WriteLine(m.ProjectionTime);
            }

            
            IList<MovieGetDetailsRes> movies = await _moviesRepository.GetAllMoviesAsync();
            var sortedMov = movies.OrderByDescending(id => id.Id).ToList();
            foreach (var m in sortedMov)
            {
                Console.WriteLine(m.Name);
            }
            AdminMoviesAndProjections lista = new AdminMoviesAndProjections(movies, projections);
            return View(lista);
        }

        // GET: AdministratorController
        public ActionResult Index()
        {
            return View();
        }

        // GET:AdministratorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdministratorController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        public ActionResult AddProjection()
        {
            return View();
        }
        // POST: AdministratorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministratorController/Edit/5
        public ActionResult EditProjection(int id)
        {
            return View();
        }

        // POST:AdministratorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministratorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult DeleteMovie(int id)
        {
            return View();
        }

        public ActionResult DeleteProjection(int id)
        {
            return View();
        }
        // POST: AdministratorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
