namespace CineMagic.Facade.Models.CinemaCreditCard
{
    public class CinemaCreditCardGetDetailsRes
    {
        public int Id { get; set; }

        public long CardNumber { get; set; }

        public double Balance { get; set; }
        public string UserId { get; set; }
    }
}
