using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineMagic.Dal.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }

        [Range(10, 30, ErrorMessage = "Pogresan broj sjedista")]
        public int SeatNumber { get; set; }

        public string HallImageUrl { get; set; }

        public IEnumerable<Seat> AllSeats { get; set; }
    }
}
