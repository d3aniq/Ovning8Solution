using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Movie.Core.DTOs;
using Movie.Service.Contracts;

namespace Movie.Presentation.Controllers
{
    /// <summary>
    /// API endpoints for managing movies. All controller actions delegate work to the service layer via
    /// <see cref="IServiceManager"/>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public MoviesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Returns a paginated list of movies. Clients can provide page and pageSize query parameters.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] int page = 1, [FromQuery(Name = "pageSize")] int pageSize = 10)
        {
            var result = await _serviceManager.Movies.GetMoviesAsync(page, pageSize);
            // Include pagination metadata in header
            var paginationMetadata = new
            {
                result.TotalItems,
                result.CurrentPage,
                result.TotalPages,
                result.PageSize
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(result.Items);
        }

        /// <summary>
        /// Returns extended details for a specific movie.
        /// </summary>
        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _serviceManager.Movies.GetMovieDetailsAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDto dto)
        {
            var created = await _serviceManager.Movies.CreateMovieAsync(dto);
            return CreatedAtRoute("GetMovieById", new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieDto dto)
        {
            var success = await _serviceManager.Movies.UpdateMovieAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Applies a JSON Patch to an existing movie. Supports updating both the movie and its details.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMovie(int id, [FromBody] JsonPatchDocument<MovieDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();
            var success = await _serviceManager.Movies.PatchMovieAsync(id, patchDoc);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a movie.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var success = await _serviceManager.Movies.DeleteMovieAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}