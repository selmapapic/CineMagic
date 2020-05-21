namespace CineMagic.Dal.Entities
{
    public class GenreMovieLink
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int MovieGenreId { get; set; }

        public virtual MovieGenre MovieGenre { get; set; }
    }
}
