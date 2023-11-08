using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSpace.Models.DTOs;
using MovieSpace.Services.Abstractions;
using MovieSpace.Utilities;
using System.Net;

namespace MovieSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }


        [HttpPost("")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var createdMovie = await _movieService.CreateMovieAsync(model);

                if (createdMovie != null)
                {
                    return Ok(new Response<CreateMovieDto>
                    {
                        Data = createdMovie,
                        Code = 200,
                        Message = "Movie created",
                        Error = "",
                    });
                }
                else
                {
                    return BadRequest(new Response<CreateMovieDto>
                    {
                        Data = null,
                        Code = 500,
                        Message = "Failed to create Movie",
                        Error = "Movie creation failed."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMovie(string Id, [FromBody] UpdateMovieDto movieUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updatedMovie = await _movieService.UpdateMovieAsync(Id, movieUpdate);

                if (updatedMovie != null)
                {
                    
                    return Ok(new Response<UpdateMovieDto>
                    {
                        Data= updatedMovie,
                        Code = 200,
                        Message = "Update Successful",
                        Error = "",
                    });
                }
                else
                {
                    return BadRequest(new Response<UpdateMovieDto>
                    {
                        Data = null,
                        Code = 500,
                        Message = "Updated Failed",
                        Error = "Error while updating"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllArticles([FromQuery] FilterMoviesDto filters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var movies = await _movieService.GetAllMoviesAsync(filters);

                if (movies != null)
                {

                    return Ok(new Response<PaginatorResponseDto<IEnumerable<GetAllMoviesDto>>>
                    {
                        Data = movies,
                        Code = 200,
                        Message = "OK",
                        Error = ""
                    });
                }
                else
                {
                    return BadRequest(new Response<PaginatorResponseDto<IEnumerable<GetAllMoviesDto>>>
                    {
                        Data = null,
                        Code = 500,
                        Message = "No Movie available",
                        Error = "",
                    });
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Response<GetSingleMovieDto>>> GetSingleArticle(string Id)
        {
                var movie = await _movieService.GetSingleMovieAsync(Id);
            try
            {

                if (movie == null)
                {
                    return BadRequest(new Response<GetSingleMovieDto>
                    {
                        Data = null,
                        Code = 204,
                        Message = "Not Found",
                        Error = "Movie not found"
                    });
                }

                return Ok(new Response<GetSingleMovieDto>
                {
                    Data = movie,
                    Code = 200,
                    Message = "OK",
                    Error = "",
                });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMovie(string Id)
        {
            try
            {
                var result = await _movieService.DeleteMovieAsync(Id);

                if (result)
                {
                    return Ok(new Response<bool>
                    {
                        Code = 200,
                        Data = result,
                        Message = "Movie deleted successfully",
                        Error = "",
                    });
                }
                else
                {
                    return BadRequest(new Response<bool>
                    {
                        Code = 404,
                        Data = !result,
                        Message = "Movie not found",
                        Error = "The specified movie was not found",
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
