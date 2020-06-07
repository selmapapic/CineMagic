using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.Projection;
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



        
        /*
        public async Task<IActionResult> AddProjectione([Bind("Id,ProjectionTime, MovieId, Movie, CinemaHallId, CinemaHall, AvailableSeats")] Projection projection)
        {
            if (ModelState.IsValid)
            {
                await _projectionsRepository.AddProjection(projection);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(projection);
        }

        
        public async Task<IActionResult> EditProjection([Bind("Id,ProjectionTime, MovieId, Movie, CinemaHallId, CinemaHall, AvailableSeats")] Projection projection)
        {
            if (ModelState.IsValid)
            {
                await _projectionsRepository.EditProjection(projection);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(projection);
        }
        */
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

        public async Task<IActionResult> DeleteProjection(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProjectionGetDetailsReq req = new ProjectionGetDetailsReq();
            req.Id = id;
            Projection projection = await _projectionsRepository.GetProjectionEntityClassWithId(req);
            if (projection == null)
            {
                return NotFound();
            }

            return View(projection);
        }
        [HttpPost, ActionName("DeleteProjection1")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjection1(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

       


            if (ModelState.IsValid)
            {
                await _projectionsRepository.DeleteProjection(id);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View();
        }
       
        public ActionResult HomeAdmin()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }

        public async Task<IActionResult> AddProjections([Bind("MovieName, ProjectionTime, CinemaHallId")] ProjectionRes projection)
        {
            if (ModelState.IsValid)
            {
                await _projectionsRepository.AddProjections(projection);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(projection);
        }
        public async Task<IActionResult> EditProjections(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProjectionRes projection = await _projectionsRepository.GetProjectionById(new ProjectionGetDetailsReq { Id = id });
            return View(projection);
        }
        [HttpPost]
        public async Task<IActionResult> EditProjections(int id, [Bind("MovieName, ProjectionTime, CinemaHallId")] ProjectionRes projection)
        {
            if (ModelState.IsValid)
            {
                await _projectionsRepository.EditProjections(projection);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(projection);
        }

        public async Task<IActionResult> DeleteProjections(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            ProjectionRes projection = await _projectionsRepository.GetProjectionById(new ProjectionGetDetailsReq { Id = id });
            if (projection == null)
            {
                return NotFound();
            }

            return View(projection);
        }
        [HttpPost, ActionName("DeleteProjections1")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjections1(int id)
        {
            if (id == null)
            {
                return NotFound();
            }




            if (ModelState.IsValid)
            {
                await _projectionsRepository.DeleteProjections(id);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View();
        }
    }
}
