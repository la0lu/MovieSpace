namespace MovieSpace.Data.Entities
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime RelesedDate { get; set; }
        public int TicketPrice { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public ICollection<Genre> Genres { get; set; } 
    }
}
