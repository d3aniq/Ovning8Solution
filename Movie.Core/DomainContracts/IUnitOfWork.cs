namespace Movie.Core.DomainContracts
{
    /// <summary>
    /// Aggregates repository dependencies and exposes a single Save operation.
    /// </summary>
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IActorRepository Actors { get; }
        IReviewRepository Reviews { get; }
        IGenreRepository Genres { get; }
        /// <summary>
        /// Persists changes to the underlying storage.
        /// </summary>
        Task<int> CompleteAsync();
    }
}