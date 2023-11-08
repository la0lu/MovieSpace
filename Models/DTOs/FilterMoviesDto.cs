namespace MovieSpace.Models.DTOs
{
    public class FilterMoviesDto
    {
        public string Genre { get; set; } 
        public string Country { get; set; } 
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}
