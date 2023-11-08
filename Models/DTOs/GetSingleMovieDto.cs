using MovieSpace.Data.Entities;

namespace MovieSpace.Models.DTOs
{
    public class GetSingleMovieDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime RelesedDate { get; set; }
        public int TicketPrice { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
