using System.Collections.Generic;

namespace CineMagic.Facade.Models.Movie
{
    public class RootObject
    {
        public int Page { get; set; }
        public List<TheMovieDb> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
