namespace Movie.Core.DTOs
{
    /// <summary>
    /// DTO for movie details. Used when returning the extended details of a movie.
    /// </summary>
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public decimal Budget { get; set; }
    }
}