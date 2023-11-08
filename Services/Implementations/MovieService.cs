using Microsoft.EntityFrameworkCore;
using MovieSpace.Data.Entities;
using MovieSpace.Data.Repositories.Abstractions;
using MovieSpace.Models.DTOs;
using MovieSpace.Services.Abstractions;
using MovieSpace.Utilities;


namespace MovieSpace.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IRepository _repository;

        public MovieService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateMovieDto> CreateMovieAsync(CreateMovieDto movie)
        {
            var newMovie = new Movie
            {
                Name = movie.Name,
                Description = movie.Description,
                Country = movie.Country,
                TicketPrice = movie.TicketPrice,
                RelesedDate = movie.RelesedDate,
                Rating = movie.Rating,
                Photo = movie.Photo,
                Genres = new List<Genre>(),
            };

            try
            {
                var selectedGenres =  GetGenresByNames(movie.Genres);

                foreach (var genre in selectedGenres)
                {
                    genre.Movies.Add(newMovie);
                    newMovie.Genres.Add(genre);
                }

                await _repository.AddAsync<Movie>(newMovie);

                var newMovieData = new CreateMovieDto
                {
                    Name = newMovie.Name,
                    Description = newMovie.Description,
                    Country = newMovie.Country,
                    TicketPrice = newMovie.TicketPrice,
                    RelesedDate = newMovie.RelesedDate,
                    Rating = newMovie.Rating,
                    Photo = newMovie.Photo,
                    Genres = newMovie.Genres.Select(g => g.Name).ToList()
                };

                return newMovieData;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create the movie: " + ex.Message);
            }
          
        }

        public List<Genre> GetGenresByNames(List<string> genreNames)
        {
            if (genreNames == null || !genreNames.Any())
            {
                return new List<Genre>();
            }

            var selectedGenres = _repository
                .GetAll<Genre>()
                .AsEnumerable()
                .Where(g => genreNames.Any(name => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return selectedGenres;
        }


        public async Task<UpdateMovieDto> UpdateMovieAsync(string movieId, UpdateMovieDto movieUpdate)
        {
            try
            {
                var existingMovie = await _repository.GetByIdAsync<Movie>(movieId);

                if (existingMovie == null)
                {
                    throw new Exception($"Movie with ID {movieId} not found.");
                }

                existingMovie.Name = movieUpdate.Name ?? existingMovie.Name;
                existingMovie.Description = movieUpdate.Description ?? existingMovie.Description;
                existingMovie.Country = movieUpdate.Country ?? existingMovie.Country;
                existingMovie.TicketPrice = movieUpdate.TicketPrice;
                existingMovie.RelesedDate = movieUpdate.RelesedDate;
                existingMovie.Rating = movieUpdate.Rating;
                existingMovie.Photo = movieUpdate.Photo;
                existingMovie.Genres = movieUpdate.Genres;

                await _repository.UpdateAsync<Movie>(existingMovie);

                var updatedMovie = new UpdateMovieDto
                {
                    Name = existingMovie.Name,
                    Description = existingMovie.Description,
                    Country = existingMovie.Country,
                    TicketPrice = existingMovie.TicketPrice,
                    RelesedDate = existingMovie.RelesedDate,
                    Rating = existingMovie.Rating,
                    Photo = existingMovie.Photo,
                    Genres = existingMovie.Genres,
                };

                return updatedMovie;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a movie:" + ex.Message);
            }
        }


        public async Task<PaginatorResponseDto<IEnumerable<GetAllMoviesDto>>> GetAllMoviesAsync(FilterMoviesDto filters)
        {
            var genreFilter = !string.IsNullOrEmpty(filters.Genre);
            var countryFilter = !string.IsNullOrEmpty(filters.Country);

            var moviesQuery = await _repository.GetAllAsync<Movie>();

            if(genreFilter) moviesQuery = moviesQuery.Where(m => m.Genres.Any(g => g.Name == filters.Genre));
            if(countryFilter) moviesQuery = moviesQuery.Where(m => m.Country == filters.Country);

            var allMovies = moviesQuery.Select(m => new GetAllMoviesDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Country = m.Country,
                RelesedDate = m.RelesedDate,
                Photo = m.Photo,
                TicketPrice = m.TicketPrice,
                Rating = m.Rating,
                Genres = m.Genres,
                
            });

            var pageNum = filters.Page ?? 1;
            var pageSize = filters.Size ?? 10;

            var paginatedResponse = Helper.Paginate(allMovies, pageNum, pageSize);

            return paginatedResponse;
        }

        public async Task<GetSingleMovieDto> GetSingleMovieAsync(string movieId)
        {
            var movie = await _repository.GetByIdAsync<Movie>(movieId);

            if (movie == null) throw new Exception($"Movie wiith ID {movieId} not found.");

            var movieData = new GetSingleMovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Country = movie.Country,
                RelesedDate = movie.RelesedDate,
                Photo = movie.Photo,
                TicketPrice = movie.TicketPrice,
                Genres = movie.Genres,
                Rating = movie.Rating,
            };

            return movieData;
        }

        public async Task<bool> DeleteMovieAsync(string movieId)
        {
            var movie = await _repository.GetByIdAsync<Movie>(movieId);

            if (movie == null) throw new Exception($"Movie wiith ID {movieId} not found.");

            await _repository.DeleteAsync(movieId);

            return true;
        }
    }
}
