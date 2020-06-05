using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.Seat
{
    public class AvailableSeatGetDetailsRes
    {
        public int Id { get; set; }

        public int SeatId { get; set; }

        public int ProjectionId { get; set; }

        public String Name { get; set; }
    }
}
