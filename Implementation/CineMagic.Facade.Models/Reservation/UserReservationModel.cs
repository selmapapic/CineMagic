using CineMagic.Facade.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Facade.Models.Reservation
{
    public class UserReservationModel
    {
        public IList<TicketGetDetailsRes> Tickets { get; set; }
    }
}
