using MovieSpace.Models.DTOs;
using MovieSpace.Utilities;

namespace MovieSpace.Services.Abstractions
{
    public interface IMovieService
    {
        Task<CreateMovieDto> CreateMovieAsync(CreateMovieDto movie);
        Task<UpdateMovieDto> UpdateMovieAsync(string movieId, UpdateMovieDto movieUpdate);
        Task<PaginatorResponseDto<IEnumerable<GetAllMoviesDto>>> GetAllMoviesAsync(FilterMoviesDto filters);
        Task<GetSingleMovieDto> GetSingleMovieAsync(string movieId);
        Task<bool> DeleteMovieAsync(string movieId);
    }
}
