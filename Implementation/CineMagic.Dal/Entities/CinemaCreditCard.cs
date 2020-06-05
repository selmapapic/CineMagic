using Microsoft.AspNetCore.Identity;

namespace CineMagic.Dal.Entities
{
    public class CinemaCreditCard
    {
        public int Id { get; set; }
        public long CinemaCreditCardNumber { get; set; }
        public double Balance { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

    }
}
