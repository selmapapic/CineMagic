using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Seat;
using CineMagic.Facade.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.Reservation
{
    public class ReservationGetDetailsRes
    {
        public AvailableSeatGetDetailsRes Seat { get; set; }
        public ProjectionGetDetailsRes Projection { get; set; }
    }
}
