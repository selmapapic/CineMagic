using System.ComponentModel.DataAnnotations;

namespace CineMagic.Dal.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        public int CinemaHallId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
