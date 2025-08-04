using Movie.Core.DomainContracts;

namespace Movie.Data.Repositories
{
    /// <summary>
    /// Aggregates all repositories and coordinates saving changes.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;
        private MovieRepository? _movies;
        private ActorRepository? _actors;
        private ReviewRepository? _reviews;
        private GenreRepository? _genres;

        public UnitOfWork(MovieContext context)
        {
            _context = context;
        }

        public IMovieRepository Movies => _movies ??= new MovieRepository(_context);
        public IActorRepository Actors => _actors ??= new ActorRepository(_context);
        public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);
        public IGenreRepository Genres => _genres ??= new GenreRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}