using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;

namespace Movie.Data
{
    /// <summary>
    /// Entity Framework Core database context used for persisting movie data.
    /// </summary>
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<VideoMovie> Movies { get; set; } = null!;
        public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<MovieDetails> MovieDetails { get; set; } = null!;
        public DbSet<MovieActor> MovieActors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many‑to‑many join table
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            // Configure one‑to‑one relationship between Movie and MovieDetails
            modelBuilder.Entity<VideoMovie>()
                .HasOne(m => m.MovieDetails)
                .WithOne(md => md.Movie)
                .HasForeignKey<MovieDetails>(md => md.MovieId);

            // Configure one‑to‑many between Genre and Movie
            modelBuilder.Entity<VideoMovie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId);

            // Seed some genres to fulfil the normalisation requirement
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Documentary" },
                new Genre { Id = 2, Name = "Action" },
                new Genre { Id = 3, Name = "Drama" }
            );
        }
    }
}