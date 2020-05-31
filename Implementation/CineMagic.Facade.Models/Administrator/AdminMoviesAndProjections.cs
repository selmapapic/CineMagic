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
        public IList<ProjectionGetDetailsRes> AdminProjections { get; set; }
        public IList<MovieGetDetailsRes> AdminMovies { get; set; }
        public AdminMoviesAndProjections(IList<MovieGetDetailsRes> mov, IList<ProjectionGetDetailsRes> pro)
        {
            AdminMovies = mov;
            AdminProjections = pro;
        }

    }
}
