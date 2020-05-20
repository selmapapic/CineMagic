using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CinemaRes.Dal.Entities
{
    public class Projection
    {
        public int Id { get; set; }

        [Required]
        public DateTime ProjectionTime { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int CinemaHallId { get; set; }

        public virtual CinemaHall CinemaHall { get; set; }

        public  virtual IEnumerable<AvailableSeat> AvailableSeats { get; set; }

    }
}
