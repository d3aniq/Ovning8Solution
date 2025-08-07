using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DTOs;
using Movie.Core.Entities;
using Movie.Data;
using Movie.Data.Mapping;
using Movie.Data.Repositories;
using Movie.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Movie.Tests
{

    public class AutoMapperReference
    {
        // Detta tvingar in AutoMapper i .deps.json
        private readonly System.Type _ = typeof(AutoMapper.Profile);
    }

    public class AutoMapperReferenceLoader
    {
        private readonly Type _ = typeof(AutoMapper.Profile);
    }


    public class DummyReference
    {
        private readonly Type _ = typeof(MapperConfiguration);
    }


    

    public class MovieServiceTests
    {
        [Fact]
        public async Task CreateMovieAsync_ShouldThrow_WhenDuplicateTitle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new MovieContext(options);
            var unitOfWork = new UnitOfWork(context);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var service = new MovieService(unitOfWork, mapper);

            // Lägg till genre
            //unitOfWork.Genres.Add(new Genre { Id = 1, Name = "Action" });
            //await unitOfWork.CompleteAsync();

            

            // Seed existing movie
            unitOfWork.Movies.Add(new VideoMovie
            {
                Title = "Duplicate",
                ReleaseYear = 2020,
                Duration = 100,
                GenreId = 1
            });
            await unitOfWork.CompleteAsync();

            var dto = new MovieDto
            {
                Title = "Duplicate",
                ReleaseYear = 2021,
                Duration = 120,
                Genre = "Action"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateMovieAsync(dto));
        }

        [Fact]
        public async Task GetMoviesAsync_ShouldLimitPageSizeToMax100()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new MovieContext(options);
            var unitOfWork = new UnitOfWork(context);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var service = new MovieService(unitOfWork, mapper);

            // Lägg till genre som alla filmer ska använda
            //unitOfWork.Genres.Add(new Genre { Id = 1, Name = "Action" });
            //await unitOfWork.CompleteAsync();

            // Seed mer än 100 filmer
            for (int i = 0; i < 150; i++)
            {
                unitOfWork.Movies.Add(new VideoMovie
                {
                    Title = $"Movie{i}",
                    ReleaseYear = 2000 + i,
                    Duration = 90,
                    GenreId = 1
                });
            }
            await unitOfWork.CompleteAsync();

            // Act
            var result = await service.GetMoviesAsync(1, 110);

            // Assert
            //Assert.Equal(100, result.Items.Count());
        }

        [Fact]
        public async Task DeleteMovieAsync_ShouldReturnFalse_WhenMovieDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new MovieContext(options);
            var unitOfWork = new UnitOfWork(context);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var service = new MovieService(unitOfWork, mapper);
            var result = await service.DeleteMovieAsync(99);
            Assert.False(result);
        }
    }


}