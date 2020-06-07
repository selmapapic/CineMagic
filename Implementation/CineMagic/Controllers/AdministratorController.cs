using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CineMagic.Dal.Context;
using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.Actor;
using CineMagic.Facade.Models.Administrator;
using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CineMagic.Controllers
{
    public class AdministratorController : Controller
    {

        private readonly ILogger<AdministratorController> _logger;
        private IMoviesRepository _moviesRepository;
        private IProjectionsRespository _projectionsRepository;
        private CineMagicDbContext _dbContext;

        public AdministratorController(ILogger<AdministratorController> logger, IMoviesRepository moviesRepository, IProjectionsRespository projectionsRepository, CineMagicDbContext dbContext)
        {
            _logger = logger;
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
            _dbContext = dbContext;
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
            return RedirectToAction("EditProjection", "Projections");
        }
        public ActionResult DeleteProjection(int id)
        {
            return RedirectToAction("DeleteProjection", "Projections");
        }
        public ActionResult DeleteMovie(int id)
        {
            return RedirectToAction("DeleteMovie", "Movies");
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

        public async Task AddMovie(MovieSearch movieSearch)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var baseAddress = new Uri("http://api.themoviedb.org/3/");
            TheMovieDb theMovieDb = new TheMovieDb
            {

            };


            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");


                // api_key can be requestred on TMDB website
                using (var response = await httpClient.GetAsync("search/movie?api_key=c1a1318b6fec8983ec3d16aa9efa6b79&query=" + movieSearch.Search))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<RootObject>(responseData);

                    foreach (var result in model.Results)
                    {
                        theMovieDb = result;
                        break;

                    }
                }
            }

            await Add(theMovieDb, movieSearch);
            
            //sad ovdje pozvati add metodu u koju ces proslijediti Add(theMovieDb, movieSearch.Trailer, movieSearch.Duration)
            // u toj metodi kreirati Movie movie = new Movie i dodijeliti sve podatke koje imas 
        }

        public async Task Add(TheMovieDb theMovieDb, MovieSearch movieSearch)
        {

            Movie movie = new Movie
            {
                Name = theMovieDb.Original_title,
                Synopsis = theMovieDb.Overview,
                Duration = movieSearch.Duration,
                TrailerUrl = movieSearch.Trailer,
                PosterUrl = movieSearch.Poster,
                Director = "director"

            };

            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            await AddCast(theMovieDb);


        }

        public async Task AddCast(TheMovieDb theMovieDb)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var baseAddress = new Uri("http://api.themoviedb.org/3/");
            List<ActorDb> actors = new List<ActorDb> { };

            ///movie/671/credits?api_key=c1a1318b6fec8983ec3d16aa9efa6b79
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");


                // api_key can be requestred on TMDB website
                using (var response = await httpClient.GetAsync("movie/" + theMovieDb.Id + "/credits?api_key=c1a1318b6fec8983ec3d16aa9efa6b79"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<RootObjectActors>(responseData);

                    foreach (var result in model.Results)
                    {
                        actors.Add(result);
                        if (actors.Count == 5) break;

                    }
                }
            }


            Movie movie = await _dbContext.Movies
                .Where(m => m.Name == theMovieDb.Name)
                .FirstOrDefaultAsync();

            foreach(var actor in actors)
            {
                Actor ac = await _dbContext.Actors
                    .Where(a => a.Name == actor.Name)
                    .FirstOrDefaultAsync();

                if(ac == null)
                {
                    Actor newActor = new Actor
                    {
                        Name = actor.Name
                    };

                    _dbContext.Actors.Add(newActor);
                    await _dbContext.SaveChangesAsync();

                }

                Actor addedActor = await _dbContext.Actors
                        .Where(a => a.Name == actor.Name)
                        .FirstOrDefaultAsync();

                ActorMovieLink aml = new ActorMovieLink
                {
                    MovieId = movie.Id,
                    ActorId = addedActor.Id
                };

                _dbContext.ActorMovieLinks.Add(aml);

            }

        }
    }
}
