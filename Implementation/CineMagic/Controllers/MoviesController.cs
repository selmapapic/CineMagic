using CineMagic.Dal.Context;
using CineMagic.Dal.Entities;
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
        private IUserRepository _userRepository;

        public MoviesController(IMoviesRepository moviesRepository, IProjectionsRespository projectionsRespository, IUserRepository userRepository)
        {
            this._moviesRepository = moviesRepository;
            this._projectionsRespository = projectionsRespository;
            this._userRepository = userRepository;
        }
       
        public async Task<IActionResult> Details(MovieGetDetailsReq req)
        {
            MovieRes res = await _moviesRepository.GetDetailsAsync(req);
            res.Projections = res.Projections.OrderBy(p => p.ProjectionTime).ToList();
            return View(res);

        }

        public async Task<IActionResult> AllMovies()
        {
            IList<MovieRes> movies = await _moviesRepository.GetAllMoviesAsync();

            return View(movies);
        }

        
        public async Task<IActionResult> MovieReservation(ProjectionGetDetailsReq req)
        {
            if (await _userRepository.DoesUserExists())
            {
                ProjectionRes res = await _projectionsRespository.GetProjectionById(req);
                return View(res);

            }
            else
            {
                return View("UserDoesNotExist");
            }


        }
        
        public async Task<IActionResult> AddMovie([Bind("Id,Name,GenreNames,ProjectionsDateTime,Duration,Synopsis,ActorNames,Director,TrailerURL,PosterUrl")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _moviesRepository.AddMovie(movie);
                
                return RedirectToAction("HomeAdmin", "Administrator");
                
                
            }
            return View(movie);
        }

        public IActionResult DeleteMovie()
        {
            return View();
        }
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieRes movie = await _moviesRepository.GetDetailsAsync(new MovieGetDetailsReq(id));
            if (_moviesRepository.DeleteMovie(movie).IsCompleted)
            {
                return RedirectToAction("HomeAdmin", "Administrator");
            }
            return View(movie);
        }
        public IActionResult HomeAdmin()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }

    }
}
