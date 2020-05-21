using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineMagic.Dal.Entities
{
    public class MovieGenre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<GenreMovieLink> MovieLinks { get; set; }
    }
}
