namespace Movie.Core.Entities
{
    /// <summary>
    /// Represents a review of a movie.
    /// </summary>
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public int MovieId { get; set; }
        public VideoMovie? Movie { get; set; }
    }
}