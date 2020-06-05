using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Projection
{
    public class ProjectionGetDetailsByMovieIdReq
    {
        [Required]
        public int? MovieId { get; set; }
    }
}
