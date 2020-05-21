using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Movie
{
    public class MovieGetDetailsReq
    {
        [Required]
        public int? Id { get; set; }
    }
}
