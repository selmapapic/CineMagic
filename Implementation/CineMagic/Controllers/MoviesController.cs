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
                res.AvailableSeats = res.AvailableSeats.OrderBy(r => r.Name).ToList();
                return View(res);

            }
            else
            {
                return View("UserDoesNotExist");
            }


        }
        
        
        public IActionResult HomeAdmin()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }

        public async Task<IActionResult> AddMovies([Bind("Id,Name,Duration,Synopsis,Director,TrailerUrl,PosterUrl,CheckBoxes, Actor1,  Actor2,  Actor3,  Actor4")] MovieRes movie)
        {
            movie.AllGenreNames = await _moviesRepository.GetMovieGenres();
            //movie.CheckBoxes = new CheckBoxModel[movie.AllGenreNames.Count];
            //for (var i = 0; i < movie.AllGenreNames.Count; i++)
            //{
            //    movie.CheckBoxes[i] = new CheckBoxModel
            //    {
            //        Id = movie.AllGenreNames[i].Id,
            //        IsSelected = false
            //    };
            //}

                if (ModelState.IsValid)
            {
            //    for( var i = 0;i<movie.AllGenreNames.Count;i++)
            //    {
            //        if (movie.AllGenreNames[i].Id == movie.CheckBoxes[i].Id && movie.CheckBoxes[i].IsSelected)
            //        {
            //            movie.GenreNames.Add(movie.AllGenreNames[i].Name);
            //        }
            //    }

                await _moviesRepository.AddMovies(movie);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(movie);
        }
        public async Task<IActionResult> DeleteMovies(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieRes movie = await _moviesRepository.GetDetailsAsync(new MovieGetDetailsReq(id));
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        [HttpPost, ActionName("DeleteMovies1")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovies1(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //MovieRes movie = await _moviesRepository.GetDetailsAsync(new MovieGetDetailsReq(id));
            //if (_moviesRepository.DeleteMovie(movie).IsCompleted)
            //{
            //    return RedirectToAction("HomeAdmin", "Administrator");
            // }
            //return View(movie);
            MovieRes movie = await _moviesRepository.GetDetailsAsync(new MovieGetDetailsReq(id));


            if (ModelState.IsValid)
            {
                await _moviesRepository.DeleteMovies(id);

                return RedirectToAction("HomeAdmin", "Administrator");


            }
            return View(movie);
        }

        
    }
}

