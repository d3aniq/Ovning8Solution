using System.Collections.Generic;

namespace Movie.Core.Entities
{
    /// <summary>
    /// Represents an actor. An actor may appear in many movies through the MovieActor join entity.
    /// </summary>
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}