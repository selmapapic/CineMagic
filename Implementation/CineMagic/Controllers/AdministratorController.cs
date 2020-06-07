using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CineMagic.Dal.Context;
using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.Actor;
using CineMagic.Facade.Models.Administrator;
using CineMagic.Facade.Models.Genre;
using CineMagic.Facade.Models.CinemaCreditCard;
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
        private IUserRepository _userRepository;

        public AdministratorController(ILogger<AdministratorController> logger, IMoviesRepository moviesRepository, IUserRepository userRepository, IProjectionsRespository projectionsRepository, CineMagicDbContext dbContext)
        {
            _logger = logger;
            this._moviesRepository = moviesRepository;
            this._projectionsRepository = projectionsRepository;
            this._userRepository = userRepository;
            _dbContext = dbContext;
        }



        public async Task<IActionResult> HomeAdmin()
        {
            IList<ProjectionRes> projections = await _projectionsRepository.GetProjections();
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

        public IActionResult AddMovies()
        {
            return View();
        }
        public ActionResult AddProjection()
        {
            return RedirectToAction("AddProjections", "Projections");
        }

        public ActionResult EditProjection(int id)
        {
            return RedirectToAction("EditProjections", "Projections", new { id = id });
        }
        public ActionResult DeleteProjection(int id)
        {
            return RedirectToAction("DeleteProjections", "Projections", new { id = id });
        }
        public ActionResult DeleteMovie(int id)
        {
            return RedirectToAction("DeleteMovies", "Movies", new { id = id });
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

        public async Task<IActionResult> AddMovie(MovieSearch movieSearch)
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
            return RedirectToAction("HomeAdmin", "Administrator");


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

        public async Task<IActionResult> AddCast(TheMovieDb theMovieDb)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var baseAddress = new Uri("http://api.themoviedb.org/3/");
            List<ActorDb> actors = new List<ActorDb> { };

            string directorName = "";

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");


                // api_key can be requestred on TMDB website
                using (var response = await httpClient.GetAsync("movie/" + theMovieDb.Id + "/credits?api_key=c1a1318b6fec8983ec3d16aa9efa6b79"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<RootObjectActors>(responseData);

                    foreach (var result in model.Cast)
                    {
                        actors.Add(result);
                        if (actors.Count == 5) break;

                    }

                    foreach(var result in model.Crew)
                    {
                        if(result.Job == "Director")
                        {
                            directorName = result.Name;
                        }
                    }
                }
            }


            Movie movie = await _dbContext.Movies
                .Where(m => m.Name == theMovieDb.Original_title)
                .FirstOrDefaultAsync();

            movie.Director = directorName;

            foreach (var actor in actors)
            {
                Actor ac = await _dbContext.Actors
                    .Where(a => a.Name == actor.Name)
                    .FirstOrDefaultAsync();

                if (ac == null)
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

           await _dbContext.SaveChangesAsync();
           await AddGenres(theMovieDb, movie.Id);
            return RedirectToAction("HomeAdmin", "Administrator");

        }

        public async Task<IActionResult> AddGenres(TheMovieDb theMovieDb, int movieId)
        {
            foreach(var gId in theMovieDb.Genre_ids)
            {
                string genreName = await GetGenreNameById((Int64)gId);


                MovieGenre mg = await _dbContext.MovieGenres
                    .Where(mg => mg.Name == genreName)
                    .FirstOrDefaultAsync();

                Movie movie = await _dbContext.Movies
                    .Where(m => m.Name == theMovieDb.Original_title)
                    .FirstOrDefaultAsync();

                GenreMovieLink gml = new GenreMovieLink
                {
                    MovieId = movie.Id,
                    MovieGenreId = mg.Id
                };

                _dbContext.GenreMovieLinks.Add(gml);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("HomeAdmin", "Administrator");
        }

        public async Task<string> GetGenreNameById(long id)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var baseAddress = new Uri("http://api.themoviedb.org/3/");
            List<GenreDb> genres = new List<GenreDb> { };

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");


                // api_key can be requestred on TMDB website
                using (var response = await httpClient.GetAsync("genre/movie/list?api_key=c1a1318b6fec8983ec3d16aa9efa6b79"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<RootObjectGenre>(responseData);

                    foreach (var result in model.Genres)
                    {
                        genres.Add(result);
                    }
                }
            }

            foreach(var g in genres)
            {
                if (g.Id == id)
                    return g.Name;
            }

            return "";
        }

        public async Task<IActionResult> AddCreditCard()
        {
            return View();
        }


        public async Task<IActionResult> AddCreditCardDb(CreditCardModel res)
        {

            await _userRepository.AddCreditCard(res);

            CinemaCreditCardGetDetailsRes created = await _userRepository.GetUserCreditCard(res);

            return View("CardAdded", (object)created);

        }

        public async Task<IActionResult> AddFunds()
        {
            return View();
        }

        public async Task<IActionResult> AddFundsDb(AddFundsModel res)
        {

            await _userRepository.AddFunds(res);

            return RedirectToAction("HomeAdmin", "Administrator");

        }
        
    }
}
