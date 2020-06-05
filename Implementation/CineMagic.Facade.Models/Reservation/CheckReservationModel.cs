namespace CineMagic.Facade.Models.Reservation
{
    public class CheckReservationModel
    {
        public string cardNumber { get; set; }
        public int AvailableSeatId { get; set; }
        public int ProjectionId { get; set; }
        public string Error { get; set; }
    }
}
