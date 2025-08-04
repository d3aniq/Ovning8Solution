using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
using Movie.Core.Entities;

namespace Movie.Data.Repositories
{
    /// <summary>
    /// Concrete repository for movies using Entity Framework Core.
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VideoMovie>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<VideoMovie?> GetAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieDetails)
                .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

        public void Add(VideoMovie movie)
        {
            _context.Movies.Add(movie);
        }

        public void Update(VideoMovie movie)
        {
            _context.Movies.Update(movie);
        }

        public void Remove(VideoMovie movie)
        {
            _context.Movies.Remove(movie);
        }
    }
}