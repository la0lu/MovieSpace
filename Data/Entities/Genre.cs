namespace MovieSpace.Data.Entities
{
    public class Genre : BaseEntity
    {
        public Genre()
        {
            Movies = new List<Movie>();
        }
        public string Name { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set;}
    }
}
