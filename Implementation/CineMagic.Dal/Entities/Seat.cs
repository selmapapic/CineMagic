using System.ComponentModel.DataAnnotations;

namespace CineMagic.Dal.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
