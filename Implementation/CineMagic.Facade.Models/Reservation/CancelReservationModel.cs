using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.Reservation
{
    public class CancelReservationModel
    {
        public int TicketId { get; set; }
        public int ProjectionId { get; set; }

        public string SeatName { get; set; }

        public int SeatId { get; set; }
    }
}
