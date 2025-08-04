using System.Collections.Generic;

namespace Movie.Core.DTOs
{
    /// <summary>
    /// Extended movie DTO including related actors, reviews and details.
    /// </summary>
    public class MovieDetailDto : MovieDto
    {
        public MovieDetailsDto? MovieDetails { get; set; }
        public List<ActorDto> Actors { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}