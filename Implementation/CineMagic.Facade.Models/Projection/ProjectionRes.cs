using CineMagic.Dal.Entities;
using CineMagic.Facade.Models.CinemaHall;
using CineMagic.Facade.Models.Movie;
using CineMagic.Facade.Models.Seat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Projection
{
    public class ProjectionRes
    {
        public int Id { get; set; }
        [Required]
        public DateTime ProjectionTime { get; set; }
        [Required]
        public string MovieName { get; set; }

        public int MovieId { get; set; }
        [Required]
        public int CinemaHallId { get; set; }
        public CinemaHallGetDetailsRes CinemaHall { get; set; }

        public IList<AvailableSeatGetDetailsRes> AvailableSeats { get; set; }

        public string PosterURL { get; set; }

         public IList<CinemaHallGetDetailsRes> AllCinemaHalls { get; set; }
    }
}
