using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineMagic.Dal.Entities;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Controllers
{
    public class ProjectionsController : Controller
    {
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;

        public ProjectionsController(IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository)
        {
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
        }
        // GET: ProjectionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProjectionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        // POST: ProjectionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProjection([Bind("Id,ProjectionTime, MovieId, MOvie, CinemaHallId, CinemaHall, AvailableSeats")] Projection projection)
        {
            if (ModelState.IsValid)
            {
                await _projectionsRepository.AddProjection(projection);
                return RedirectToAction("HomeAdmin", "Administrator");
            }
            return View(projection);
        }

        // GET: ProjectionsController/Edit/5
        public ActionResult EditProjection(int id)
        {
            return View();
        }

        // POST:ProjectionsController/Edit/5
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

        // GET:ProjectionsController/Delete/5
        public ActionResult DeleteProjection(int id)
        {
            return View();
        }

        // POST: ProjectionsController/Delete/5
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
        public ActionResult HomeAdmin()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }
    }
}
