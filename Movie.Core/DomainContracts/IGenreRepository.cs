using Movie.Core.Entities;

namespace Movie.Core.DomainContracts
{
    /// <summary>
    /// Repository abstraction for genre operations.
    /// </summary>
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetAsync(int id);
        Task<Genre?> GetByNameAsync(string name);
    }
}