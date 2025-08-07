using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie.Data;
using Movie.Data.Extensions;         // för AddDataAccess
using Movie.Data.Mapping;            // för MappingProfile
using Movie.Services;                // för ServiceManager och mappning
using Movie.Service.Contracts;        // <-- Lägger till ISeviceManager


var builder = WebApplication.CreateBuilder(args);

// Lägg till DbContext med connection string från appsettings.json
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Lägg till kontroller
builder.Services.AddControllers();

// Lägg till swagger (om du använder det)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// registrera databasen och UnitOfWork; faller tillbaka på in?memory om ingen connection?string finns
builder.Services.AddDataAccess(builder.Configuration);

// registrera tjänstelagret (ServiceManager skapar IMovieService, IActorService, m.m.)
builder.Services.AddScoped<IServiceManager, ServiceManager>();

// registrera AutoMapper med din MappingProfile
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// se till att controllers i Movie.Presentation laddas in
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Movie.Presentation.Controllers.MoviesController).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
