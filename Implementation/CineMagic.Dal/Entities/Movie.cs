using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineMagic.Dal.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 300, ErrorMessage = "Pogresna duzina")]
        public int Duration { get; set; }
        [Required]
        public string Synopsis { get; set; }
        [Required]
        public string Director { get; set; }

        public string TrailerUrl { get; set; }
        [Required]
        public string PosterUrl { get; set; }

        public virtual IEnumerable<GenreMovieLink> GenreLinks { get; set; }

        public virtual IEnumerable<ActorMovieLink> ActorMovieLinks { get; set; }
    }
}
