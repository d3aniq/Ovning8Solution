using AutoMapper;
using Movie.Core.DomainContracts;
using Movie.Service.Contracts;

namespace Movie.Services
{
    /// <summary>
    /// Aggregates concrete service implementations and lazily instantiates them when first accessed.
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IReviewService> _reviewService;
        private readonly Lazy<IGenreService> _genreService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _movieService = new Lazy<IMovieService>(() => new MovieService(unitOfWork, mapper));
            _actorService = new Lazy<IActorService>(() => new ActorService(unitOfWork, mapper));
            _reviewService = new Lazy<IReviewService>(() => new ReviewService(unitOfWork, mapper));
            _genreService = new Lazy<IGenreService>(() => new GenreService(unitOfWork));
        }

        public IMovieService Movies => _movieService.Value;
        public IActorService Actors => _actorService.Value;
        public IReviewService Reviews => _reviewService.Value;
        public IGenreService Genres => _genreService.Value;
    }
}