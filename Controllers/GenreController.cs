using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSpace.Data.Entities;
using MovieSpace.Models.DTOs;
using MovieSpace.Services.Abstractions;
using MovieSpace.Utilities;

namespace MovieSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenreController> _logger;
        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [HttpPost("genres")]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto model)
        {
            try
            {
                var genre = await _genreService.CreateGenreAsync(model);

                return Ok(new Response<Genre>
                {
                    Code = 200,
                    Data = genre,
                    Message = "OK",
                    Error = "",
                });
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "An error occurred while creating a genre.");
                return BadRequest("Failed to create Genre: " + ex.Message);
            }
        }

    }
}
