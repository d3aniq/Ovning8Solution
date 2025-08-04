using Movie.Core.Entities;

namespace Movie.Core.DomainContracts
{
    /// <summary>
    /// Repository abstraction for movie aggregate operations.
    /// </summary>
    public interface IMovieRepository
    {
        /// <summary>
        /// Returns a paginated list of movies.
        /// </summary>
        /// <param name="page">The page number (1-based).</param>
        /// <param name="pageSize">The maximum number of items per page.</param>
        Task<IEnumerable<VideoMovie>> GetAllAsync(int page, int pageSize);
        /// <summary>
        /// Finds a movie by its identifier including related entities.
        /// </summary>
        /// <param name="id">The movie identifier.</param>
        Task<VideoMovie?> GetAsync(int id);
        /// <summary>
        /// Determines whether a movie exists.
        /// </summary>
        Task<bool> AnyAsync(int id);
        /// <summary>
        /// Adds a new movie to the underlying storage context.
        /// </summary>
        void Add(VideoMovie movie);
        /// <summary>
        /// Updates an existing movie in the underlying storage context.
        /// </summary>
        void Update(VideoMovie movie);
        /// <summary>
        /// Removes a movie from the underlying storage context.
        /// </summary>
        void Remove(VideoMovie movie);
    }
}