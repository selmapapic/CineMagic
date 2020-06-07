using CineMagic.Facade.Models.Seat;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.CinemaHall
{
    public class CinemaHallGetDetailsRes
    {
        public int Id { get; set; }

        public string HallImageUrl { get; set; }

        public IList<AvailableSeatGetDetailsRes> AllSeats { get; set; }

        public int numberOfSeats { get; set; }
    }
}
