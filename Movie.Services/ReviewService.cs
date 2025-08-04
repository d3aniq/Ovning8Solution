using AutoMapper;
using Movie.Core.DomainContracts;
using Movie.Core.DTOs;
using Movie.Core.Entities;
using Movie.Service.Contracts;

namespace Movie.Services
{
    /// <summary>
    /// Implementation of <see cref="IReviewService"/> for managing movie reviews.
    /// </summary>
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId)
        {
            var reviews = await _unitOfWork.Reviews.GetByMovieAsync(movieId);
            return reviews.Select(r => _mapper.Map<ReviewDto>(r)).ToList();
        }

        public async Task<ReviewDto> AddReviewAsync(int movieId, ReviewDto dto)
        {
            // Business rule: a movie may have max 10 reviews
            var reviews = await _unitOfWork.Reviews.GetByMovieAsync(movieId);
            if (reviews.Count() >= 10)
            {
                throw new InvalidOperationException("A movie cannot have more than 10 reviews.");
            }
            var review = new Review
            {
                Comment = dto.Text,
                Rating = dto.Rating,
                MovieId = movieId,
                ReviewerName = string.Empty
            };
            _unitOfWork.Reviews.Add(review);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReviewDto>(review);
        }
    }
}