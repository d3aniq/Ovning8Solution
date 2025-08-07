using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;

namespace Movie.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        // Fallback-konstruktor för design-time (t.ex. migrationer)
        public MovieContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fallback-anslutning används vid migration om ingen DI är konfigurerad
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MovieDb;Trusted_Connection=True;");
            }
        }

        public DbSet<VideoMovie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieDetails>()
                .HasOne(md => md.Genre)
                .WithMany()
                .HasForeignKey(md => md.GenreId)
                .OnDelete(DeleteBehavior.Restrict); // eller .NoAction


            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(
        new Genre { Id = 1, Name = "Action" },
        new Genre { Id = 2, Name = "Comedy" },
        new Genre { Id = 3, Name = "Drama" }
    );


            modelBuilder.Entity<VideoMovie>().HasData(
       new VideoMovie { Id = 1, Title = "Fast & Curious", ReleaseYear = 2020, Duration = 120, GenreId = 1 },
       new VideoMovie { Id = 2, Title = "Laugh Factory", ReleaseYear = 2019, Duration = 95, GenreId = 2 }
   );

            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, MovieId = 1, ReviewerName = "Alice", Comment = "Loved it", Rating = 5 },
                new Review { Id = 2, MovieId = 2, ReviewerName = "Bob", Comment = "Funny but shallow", Rating = 3 }
            );

            //modelBuilder.Entity<Actor>().HasData(
            //    new Actor { Id = 1, Name = "Tom Hanks", BirthYear = 1956 },
            //    new Actor { Id = 2, Name = "Meryl Streep", BirthYear = 1949 }
            //);

            //modelBuilder.Entity<Genre>().HasData(
            //    new Genre { Id = 1, Name = "Drama" },
            //    new Genre { Id = 2, Name = "Comedy" }
            //);
        }
    }
}
