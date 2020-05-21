using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.Movie
{
    public class MovieGetDetailsRes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<string> GenreNames { get; set; }

        public int Duration { get; set; }
        public string Synopsis { get; set; }
        public IList<string> ActorNames { get; set; }
        public string Director { get; set; }
        public string TrailerURL { get; set; }
        public string PosterURL { get; set; }
    }
}
