using Movie.Core.DTOs;

namespace Movie.Service.Contracts
{
    /// <summary>
    /// Service abstraction for genre operations.
    /// </summary>
    public interface IGenreService
    {
        Task<IEnumerable<string>> GetGenresAsync();
        Task<int?> GetGenreIdByNameAsync(string name);
    }
}