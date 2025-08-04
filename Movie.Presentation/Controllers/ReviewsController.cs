using Microsoft.AspNetCore.Mvc;
using Movie.Core.DTOs;
using Movie.Service.Contracts;

namespace Movie.Presentation.Controllers
{
    /// <summary>
    /// API endpoints for managing reviews associated with a specific movie.
    /// </summary>
    [ApiController]
    [Route("api/movies/{movieId}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ReviewsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(int movieId)
        {
            var reviews = await _serviceManager.Reviews.GetReviewsAsync(movieId);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int movieId, [FromBody] ReviewDto dto)
        {
            var created = await _serviceManager.Reviews.AddReviewAsync(movieId, dto);
            return CreatedAtAction(nameof(GetReviews), new { movieId = movieId }, created);
        }
    }
}