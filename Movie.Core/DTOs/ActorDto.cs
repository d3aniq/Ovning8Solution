namespace Movie.Core.DTOs
{
    /// <summary>
    /// Data transfer object representing an actor. Contains only the fields
    /// that should be exposed to API consumers.
    /// </summary>
    public class ActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}