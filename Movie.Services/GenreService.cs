using AutoMapper;
using Movie.Core.DomainContracts;
using Movie.Service.Contracts;

namespace Movie.Services
{
    /// <summary>
    /// Implementation of <see cref="IGenreService"/> to query available genres.
    /// </summary>
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> GetGenresAsync()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return genres.Select(g => g.Name).ToList();
        }

        public async Task<int?> GetGenreIdByNameAsync(string name)
        {
            var genre = await _unitOfWork.Genres.GetByNameAsync(name);
            return genre?.Id;
        }
    }
}