using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Movie.Core;
using Movie.Core.DomainContracts;
using Movie.Core.DTOs;
using Movie.Core.Entities;
using Movie.Service.Contracts;

namespace Movie.Services
{
    /// <summary>
    /// Implementation of the <see cref="IMovieService"/> using repositories and AutoMapper.
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<MovieDto>> GetMoviesAsync(int page, int pageSize)
        {
            // Ensure sensible defaults and limits
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            if (page <= 0) page = 1;

            // Query the repository
            var movies = await _unitOfWork.Movies.GetAllAsync(page, pageSize);
            // NOTE: In a real implementation the total count should be retrieved separately to compute total pages
            var totalCount = movies.Count();

            var dtoList = movies.Select(m => _mapper.Map<MovieDto>(m)).ToList();
            var result = new PagedResult<MovieDto>
            {
                Items = dtoList,
                TotalItems = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            return result;
        }

        public async Task<MovieDetailDto?> GetMovieDetailsAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);
            if (movie == null) return null;
            var dto = _mapper.Map<MovieDetailDto>(movie);
            // Map nested collections
            if (movie.MovieDetails != null)
            {
                dto.MovieDetails = _mapper.Map<MovieDetailsDto>(movie.MovieDetails);
            }
            dto.Actors = movie.MovieActors.Select(ma => _mapper.Map<ActorDto>(ma.Actor!)).ToList();
            dto.Reviews = movie.Reviews.Select(r => _mapper.Map<ReviewDto>(r)).ToList();
            return dto;
        }

        public async Task<MovieDto> CreateMovieAsync(MovieDto dto)
        {
            // Business rule: movie title must be unique
            var existingMovies = await _unitOfWork.Movies.GetAllAsync(1, int.MaxValue);
            if (existingMovies.Any(m => string.Equals(m.Title, dto.Title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A movie with the title '{dto.Title}' already exists.");
            }

            var entity = _mapper.Map<VideoMovie>(dto);
            // Default to documentary if no genre provided
            var genre = dto.Genre;
            if (!string.IsNullOrWhiteSpace(genre))
            {
                var g = await _unitOfWork.Genres.GetByNameAsync(genre);
                if (g == null) throw new InvalidOperationException($"Genre '{genre}' does not exist.");
                entity.GenreId = g.Id;
            }
            else
            {
                var doc = await _unitOfWork.Genres.GetByNameAsync("Documentary");
                entity.GenreId = doc?.Id ?? 1;
            }

            _unitOfWork.Movies.Add(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<MovieDto>(entity);
        }

        public async Task<bool> UpdateMovieAsync(int id, MovieDto dto)
        {
            var existing = await _unitOfWork.Movies.GetAsync(id);
            if (existing == null) return false;
            // Check for unique title (excluding current)
            var others = await _unitOfWork.Movies.GetAllAsync(1, int.MaxValue);
            if (others.Any(m => m.Id != id && string.Equals(m.Title, dto.Title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Another movie with the title '{dto.Title}' already exists.");
            }
            // Map updated properties
            existing.Title = dto.Title;
            existing.ReleaseYear = dto.ReleaseYear;
            existing.Duration = dto.Duration;
            // Update genre if provided
            if (!string.IsNullOrWhiteSpace(dto.Genre))
            {
                var g = await _unitOfWork.Genres.GetByNameAsync(dto.Genre);
                if (g == null) throw new InvalidOperationException($"Genre '{dto.Genre}' does not exist.");
                existing.GenreId = g.Id;
            }
            _unitOfWork.Movies.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> PatchMovieAsync(int id, JsonPatchDocument<MovieDto> patchDoc)
        {
            var existing = await GetMovieDetailsAsync(id);
            if (existing == null) return false;
            var dto = new MovieDto
            {
                Id = existing.Id,
                Title = existing.Title,
                ReleaseYear = existing.ReleaseYear,
                Duration = existing.Duration,
                Genre = existing.Genre
            };
            try
            {
                patchDoc.ApplyTo(dto);
            }
            catch (JsonPatchException)
            {
                throw;
            }
            // Map patched DTO back to entity
            return await UpdateMovieAsync(id, dto);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetAsync(id);
            if (movie == null) return false;
            _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}