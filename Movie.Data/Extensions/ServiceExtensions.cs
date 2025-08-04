using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Core.DomainContracts;
using Movie.Data.Repositories;

namespace Movie.Data.Extensions
{
    /// <summary>
    /// Extension methods for registering data layer services with the dependency injection container.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers the Entity Framework Core context and UnitOfWork with the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            // Use SQL Server if a connection string is provided, otherwise fall back to an in‑memory database for testing.
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<MovieContext>(options => options.UseInMemoryDatabase("MovieDb"));
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}