using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie.Data;
using Movie.Data.Extensions;         // f�r AddDataAccess
using Movie.Data.Mapping;            // f�r MappingProfile
using Movie.Services;                // f�r ServiceManager och mappning
using Movie.Service.Contracts;        // <-- L�gger till ISeviceManager


var builder = WebApplication.CreateBuilder(args);

// L�gg till DbContext med connection string fr�n appsettings.json
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// L�gg till kontroller
builder.Services.AddControllers();

// L�gg till swagger (om du anv�nder det)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// registrera databasen och UnitOfWork; faller tillbaka p� in?memory om ingen connection?string finns
builder.Services.AddDataAccess(builder.Configuration);

// registrera tj�nstelagret (ServiceManager skapar IMovieService, IActorService, m.m.)
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
