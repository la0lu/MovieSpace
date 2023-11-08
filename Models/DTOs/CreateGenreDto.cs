using System.ComponentModel.DataAnnotations;

namespace MovieSpace.Models.DTOs
{
    public class CreateGenreDto
    {
        [Required]
        public string Name { get; set; }
    }
}
