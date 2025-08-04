using Movie.Core.DTOs;

namespace Movie.Service.Contracts
{
    /// <summary>
    /// Service abstraction for review operations.
    /// </summary>
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId);
        Task<ReviewDto> AddReviewAsync(int movieId, ReviewDto dto);
    }
}