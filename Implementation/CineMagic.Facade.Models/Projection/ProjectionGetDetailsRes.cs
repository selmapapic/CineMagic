using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.CinemaHall;
using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Models.Seat;
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

        public CinemaHallGetDetailsRes CinemaHall { get; set; }

        public IList<AvailableSeatGetDetailsRes> AvailableSeats { get; set; }

        public string PosterURL { get; set; }

    }
}
