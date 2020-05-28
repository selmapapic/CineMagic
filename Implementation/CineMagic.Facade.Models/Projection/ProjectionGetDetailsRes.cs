using CineMagic.Facade.Models.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.Projection
{
    public class ProjectionGetDetailsRes
    {
        public int Id { get; set; }

        public DateTime ProjectionTime { get; set; }

        //public MovieGetDetailsRes Movie { get; set; }




        //public int CinemaHallId { get; set; }

        //public virtual CinemaHall CinemaHall { get; set; }

        //public virtual IEnumerable<AvailableSeat> AvailableSeats { get; set; }
    }
}
