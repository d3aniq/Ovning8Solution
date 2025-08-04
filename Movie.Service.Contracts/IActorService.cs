using Movie.Core;
using Movie.Core.DTOs;

namespace Movie.Service.Contracts
{
    /// <summary>
    /// Service abstraction for actor operations.
    /// </summary>
    public interface IActorService
    {
        Task<PagedResult<ActorDto>> GetActorsAsync(int page, int pageSize);
        Task<ActorDto?> GetActorAsync(int id);
        Task<ActorDto> CreateActorAsync(ActorDto dto);
        Task<bool> UpdateActorAsync(int id, ActorDto dto);
    }
}