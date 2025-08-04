using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
using Movie.Core.Entities;

namespace Movie.Data.Repositories
{
    /// <summary>
    /// Concrete repository for reviews.
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieContext _context;

        public ReviewRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetByMovieAsync(int movieId)
        {
            return await _context.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
        }
    }
}