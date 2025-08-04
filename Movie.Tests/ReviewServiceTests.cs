using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DTOs;
using Movie.Core.Entities;
using Movie.Data;
using Movie.Data.Mapping;
using Movie.Data.Repositories;
using Movie.Services;
using Xunit;

namespace Movie.Tests
{
    public class ReviewServiceTests
    {
        [Fact]
        public async Task AddReviewAsync_ShouldThrow_WhenMoreThanTenReviews()
        {
            var dummy = typeof(AutoMapper.MapperConfiguration);
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new MovieContext(options);
            var unitOfWork = new UnitOfWork(context);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var reviewService = new ReviewService(unitOfWork, mapper);
            // Seed movie
            var movie = new VideoMovie { Title = "Test", ReleaseYear = 2020, Duration = 100, GenreId = 1 };
            unitOfWork.Movies.Add(movie);
            await unitOfWork.CompleteAsync();
            // Add 10 reviews
            for (int i = 0; i < 10; i++)
            {
                unitOfWork.Reviews.Add(new Review { MovieId = movie.Id, Comment = $"Review{i}", Rating = 5, ReviewerName = $"User{i}"});
            }
            await unitOfWork.CompleteAsync();
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => reviewService.AddReviewAsync(movie.Id, new ReviewDto { Rating = 4, Text = "Another" }));
        }
    }
}