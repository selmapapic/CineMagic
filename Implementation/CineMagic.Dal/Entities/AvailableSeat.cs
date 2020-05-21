namespace CineMagic.Dal.Entities
{
    public class AvailableSeat
    {
        public int Id { get; set; }

        public int SeatId { get; set; }

        public virtual Seat Seat { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }
    }
}
