namespace Movie.Core.Entities
{
    /// <summary>
    /// Detailed information about a movie. This class has a one‑to‑one
    /// relationship with <see cref="Movie"/>.
    /// </summary>
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public decimal Budget { get; set; }
        public int MovieId { get; set; }
        public VideoMovie? Movie { get; set; }

        //public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; } // Foreign key till Genre

        public Genre Genre { get; set; }

    }
}