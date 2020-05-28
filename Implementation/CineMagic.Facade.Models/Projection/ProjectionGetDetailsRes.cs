using CineMagic.Dal.Entities;
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

        public string MovieName { get; set; }

        public int MovieId { get; set; }

        public CinemaHall CinemaHall { get; set; }

        public IList<AvailableSeat> AvailableSeats { get; set; }

    }
}
