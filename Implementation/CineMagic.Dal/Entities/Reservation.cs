using Microsoft.AspNetCore.Identity;

namespace CineMagic.Dal.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

    }
}
