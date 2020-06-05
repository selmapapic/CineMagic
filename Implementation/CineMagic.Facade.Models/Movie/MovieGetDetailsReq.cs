using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Movie
{
    public class MovieGetDetailsReq
    {
        public MovieGetDetailsReq(int id)
        {
            Id = id;
        }

        public MovieGetDetailsReq()
        {

        }

        [Required]
        public int? Id { get; set; }
    }
}
