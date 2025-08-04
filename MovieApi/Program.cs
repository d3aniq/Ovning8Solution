using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// L�gg till DbContext med connection string fr�n appsettings.json
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// L�gg till kontroller
builder.Services.AddControllers();

// L�gg till swagger (om du anv�nder det)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
