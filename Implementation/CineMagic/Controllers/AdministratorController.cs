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


      
        public async Task<IActionResult> HomeAdmin()
        {
            IList<ProjectionRes> projections = await _projectionsRepository.GetAllProjectionsAsync();
            var sortedPro = projections.OrderByDescending(time => time.ProjectionTime).ToList();

            IList<MovieRes> movies = await _moviesRepository.GetAllMoviesAsync();
            var sortedMov = movies.OrderByDescending(id => id.Id).ToList();
            
            AdminMoviesAndProjections lista = new AdminMoviesAndProjections(movies, projections);
            return View(lista);
        }

        // GET: AdministratorController
        public ActionResult Index()
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
            return RedirectToAction("AddMovie", "Movies");
        }
        public ActionResult AddProjection()
        {
            return RedirectToAction("AddProjection", "Projections");
        }

        public ActionResult EditProjection(int id)
        {
            return RedirectToAction("EditProjection", "Projections", new { @id = id });
        }
        
        public ActionResult DeleteProjection(int id)
        {
            return RedirectToAction("DeleteProjection", "Projections", new { @id = id });
        }
        public ActionResult DeleteMovie(int id)
        {
            return RedirectToAction("DeleteMovie", "Movies", new { @id = id });
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
