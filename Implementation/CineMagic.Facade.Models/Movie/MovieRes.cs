using CineMagic.Facade.Models.Projection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Movie
{
    public class MovieRes
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<string> GenreNames { get; set; }

        public IList<ProjectionRes> Projections { get; set; }

        public int Duration { get; set; }
        public string Synopsis { get; set; }
        public IList<string> ActorNames { get; set; }
        public string Director { get; set; }
        public string TrailerURL { get; set; }
        public string PosterURL { get; set; }
       // public string Actor1 { get; set; }
        //public string Actor2 { get; set; }
       // public string Actor3 { get; set; }
        //public string Actor4 { get; set; }
        public IList<GenreRes> AllGenreNames { get; set; }
        //public CheckBoxModel[] CheckBoxes { get; set; }
    }
}
