using Movie.Core.Entities;

namespace Movie.Core.DomainContracts
{
    /// <summary>
    /// Repository abstraction for review operations.
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Gets all reviews for a given movie.
        /// </summary>
        Task<IEnumerable<Review>> GetByMovieAsync(int movieId);
        /// <summary>
        /// Adds a new review to the context.
        /// </summary>
        void Add(Review review);
    }
}