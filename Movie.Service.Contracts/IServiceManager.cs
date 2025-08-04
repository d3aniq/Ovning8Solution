namespace Movie.Service.Contracts
{
    /// <summary>
    /// Aggregates all services and exposes them as properties. Controllers should depend on this
    /// abstraction instead of individual services to decouple the API layer from service implementations.
    /// </summary>
    public interface IServiceManager
    {
        IMovieService Movies { get; }
        IActorService Actors { get; }
        IReviewService Reviews { get; }
        IGenreService Genres { get; }
    }
}