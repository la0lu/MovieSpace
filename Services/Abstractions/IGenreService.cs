using MovieSpace.Data.Entities;
using MovieSpace.Models.DTOs;

namespace MovieSpace.Services.Abstractions
{
    public interface IGenreService
    {
        Task<Genre> CreateGenreAsync(CreateGenreDto genre);
    }
}
