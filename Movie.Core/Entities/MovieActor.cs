namespace Movie.Core.Entities
{
    /// <summary>
    /// Join table for the many‑to‑many relationship between movies and actors.
    /// </summary>
    public class MovieActor
    {
        public int MovieId { get; set; }
        public VideoMovie? Movie { get; set; }
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}