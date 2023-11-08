using Microsoft.EntityFrameworkCore;
using MovieSpace.Data;
using MovieSpace.Data.Repositories;
using MovieSpace.Data.Repositories.Abstractions;
using MovieSpace.Services.Abstractions;
using MovieSpace.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieSpaceContext>(options => 
{ options.UseSqlServer(builder.Configuration.GetConnectionString("default")); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
    
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
