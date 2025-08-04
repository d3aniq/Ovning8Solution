using Microsoft.AspNetCore.Mvc;
using Movie.Core.DTOs;
using Movie.Service.Contracts;

namespace Movie.Presentation.Controllers
{
    /// <summary>
    /// API endpoints for managing actors.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ActorsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetActors([FromQuery] int page = 1, [FromQuery(Name = "pageSize")] int pageSize = 10)
        {
            var result = await _serviceManager.Actors.GetActorsAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await _serviceManager.Actors.GetActorAsync(id);
            if (actor == null) return NotFound();
            return Ok(actor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActor([FromBody] ActorDto dto)
        {
            var created = await _serviceManager.Actors.CreateActorAsync(dto);
            return CreatedAtAction(nameof(GetActor), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] ActorDto dto)
        {
            var success = await _serviceManager.Actors.UpdateActorAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}