namespace Movie.Core.DTOs
{
    /// <summary>
    /// Basic movie DTO used for listing and create/update operations.
    /// </summary>
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string? Genre { get; set; }
        public int Duration { get; set; }
    }
}