using System.Collections.Generic;

namespace Movie.Core.Entities
{
    /// <summary>
    /// Represents a film in the system. A movie belongs to a genre and has optional
    /// details, many reviews and many actors through the linking entity MovieActor.
    /// </summary>
    public class VideoMovie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        /// <summary>
        /// Foreign key for the genre this movie belongs to.
        /// </summary>
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        /// <summary>
        /// Optional 1:1 relationship containing extended information about the movie.
        /// </summary>
        public MovieDetails? MovieDetails { get; set; }
        /// <summary>
        /// Collection of reviews attached to this movie. A movie can have many reviews.
        /// </summary>
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        /// <summary>
        /// Many‑to‑many relationship with actors through the MovieActor join table.
        /// </summary>
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}