using MovieSpace.Data.Entities;
using MovieSpace.Utilities;
using System.ComponentModel.DataAnnotations;

namespace MovieSpace.Models.DTOs
{
    public class UpdateMovieDto
    {
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Photo { get; set; }
        [Required] public DateTime RelesedDate { get; set; }
        [Required] public int TicketPrice { get; set; }
        [Required] public ICollection<Genre> Genres { get; set; }

        [Required]
        [CountryValidation(ErrorMessage = "Country must be either: NIGERIA, GHANA, USA, UK, KOREA, INDIA, SPAIN")]
        public string Country { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
    }
}
