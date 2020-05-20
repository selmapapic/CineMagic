using CinemaRes.Dal.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaRes.Dal.Context
{
    public class CinemaResDbContext : IdentityDbContext
    {
        public CinemaResDbContext(DbContextOptions<CinemaResDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<GenreMovieLink> GenreMovieLinks { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<ActorMovieLink> ActorMovieLinks { get; set; }
        public DbSet<AvailableSeat> AvailableSeats { get; set; }
    }
}
