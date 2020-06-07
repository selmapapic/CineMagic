using CineMagic.Facade.Models.Projection;

namespace CineMagic.Facade.Models.Ticket
{
    public class TicketGetDetailsRes
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public int SeatId { get; set; }

        public int ProjectionId { get; set; }

        public string SeatName { get; set; }

        public ProjectionRes Projection { get; set; }
    }
}
