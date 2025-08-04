using Movie.Core.Entities;

namespace Movie.Core.DomainContracts
{
    /// <summary>
    /// Repository abstraction for actor operations.
    /// </summary>
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllAsync(int page, int pageSize);
        Task<Actor?> GetAsync(int id);
        void Add(Actor actor);
        void Update(Actor actor);
    }
}