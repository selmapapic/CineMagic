using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaRes.Dal.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
