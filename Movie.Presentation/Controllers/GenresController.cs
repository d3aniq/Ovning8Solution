using Microsoft.AspNetCore.Mvc;
using Movie.Service.Contracts;

namespace Movie.Presentation.Controllers
{
    /// <summary>
    /// API endpoints for querying available genres.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public GenresController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _serviceManager.Genres.GetGenresAsync();
            return Ok(genres);
        }
    }
}