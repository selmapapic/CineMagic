namespace CineMagic.Dal.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public int SeatId { get; set; }

        public virtual Seat Seat { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }
    }
}
