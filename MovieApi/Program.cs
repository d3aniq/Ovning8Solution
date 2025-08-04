using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Data.Extensions;
using Movie.Data.Mapping;
using Movie.Service.Contracts;
using Movie.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Enable JSON Patch support

// Register AutoMapper and our profile
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register the data access layer (DbContext + UnitOfWork)
builder.Services.AddDataAccess(builder.Configuration);

// Register the service layer
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();