using CineMagic.Facade.Models.Projection;
using CineMagic.Facade.Models.Seat;

namespace CineMagic.Facade.Models.Reservation
{
    public class ReservationGetDetailsRes
    {
        public AvailableSeatGetDetailsRes Seat { get; set; }
        public ProjectionRes Projection { get; set; }

    }
}
