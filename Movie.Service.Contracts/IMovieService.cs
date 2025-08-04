using Microsoft.AspNetCore.JsonPatch;
using Movie.Core;
using Movie.Core.DTOs;

namespace Movie.Service.Contracts
{
    /// <summary>
    /// Service abstraction for operations on the Movie aggregate root.
    /// </summary>
    public interface IMovieService
    {
        Task<PagedResult<MovieDto>> GetMoviesAsync(int page, int pageSize);
        Task<MovieDetailDto?> GetMovieDetailsAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieDto dto);
        Task<bool> UpdateMovieAsync(int id, MovieDto dto);
        Task<bool> PatchMovieAsync(int id, JsonPatchDocument<MovieDto> patchDoc);
        Task<bool> DeleteMovieAsync(int id);
    }
}