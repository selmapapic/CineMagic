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
            
            AdminMoviesAndProjections lista = new AdminMoviesAndProjections(sortedMov, sortedPro);
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
            return RedirectToAction("AddProjections", "Projections");
            //ovdje mijenjao
        }

        public ActionResult EditProjection(int id)
        {
            return RedirectToAction("EditProjections", "Projections", new { @id = id });
        }
        
        public ActionResult DeleteProjection(int id)
        {
            return RedirectToAction("DeleteProjections", "Projections", new { @id = id });
        }
        public ActionResult DeleteMovie(int id)
        {
            return RedirectToAction("DeleteMovies", "Movies", new { @id = id });
        }

        
    }
}
