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

       
        public ActionResult HomeAdmin()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }

        public async Task<IActionResult> AddProjections([Bind("MovieName, ProjectionTime, CinemaHallId")] ProjectionRes projection)
        {
            projection.AllCinemaHalls = await _projectionsRepository.GetAllCinemaHalls();
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
            projection.AllCinemaHalls = await _projectionsRepository.GetAllCinemaHalls();

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



            ProjectionRes projection = await _projectionsRepository.GetProjectionById(new ProjectionGetDetailsReq { Id = id });
            if (ModelState.IsValid)
            {
                await _projectionsRepository.DeleteProjections(id);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(projection);
        }
    }
}
