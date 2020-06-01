using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Models.Projection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Models.Administrator

{
    public class AdminMoviesAndProjections
    {
        public IList<ProjectionRes> AdminProjections { get; set; }
        public IList<MovieRes> AdminMovies { get; set; }
        public AdminMoviesAndProjections(IList<MovieRes> mov, IList<ProjectionRes> pro)
        {
            AdminMovies = mov;
            AdminProjections = pro;
        }

    }
}
