using MovieSpace.Data.Entities;
using MovieSpace.Data.Repositories.Abstractions;
using MovieSpace.Models.DTOs;
using MovieSpace.Services.Abstractions;

namespace MovieSpace.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IRepository _repository;

        private readonly List<string> allowedGenres = new()
        {
            "Action",
            "Romance",
            "Adventure",
            "Comedy",
            "Drama",
            "Horror",
            "Mystery"
        };

        public GenreService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Genre> CreateGenreAsync(CreateGenreDto genre)
        {
            var newGenre = new Genre
            {
                Name = genre.Name,
            };

            await _repository.AddAsync<Genre>(newGenre);

            return newGenre;
        }
    }
}
