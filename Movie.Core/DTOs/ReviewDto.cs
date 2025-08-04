namespace Movie.Core.DTOs
{
    /// <summary>
    /// Data transfer object representing a review. Used when returning
    /// review information from the API.
    /// </summary>
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; } = null!;
    }
}