using System.Collections.Generic;

namespace Movie.Core.Entities
{
    /// <summary>
    /// Represents a film genre. A genre can be associated with many movies.
    /// </summary>
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<VideoMovie> Movies { get; set; } = new List<VideoMovie>();
    }
}