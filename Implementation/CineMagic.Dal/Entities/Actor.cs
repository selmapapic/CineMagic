using System.ComponentModel.DataAnnotations;

namespace CinemaRes.Dal.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
