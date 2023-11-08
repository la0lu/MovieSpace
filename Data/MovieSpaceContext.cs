using Microsoft.EntityFrameworkCore;
using MovieSpace.Data.Entities;

namespace MovieSpace.Data
{
    public class MovieSpaceContext : DbContext
    {
        public MovieSpaceContext(DbContextOptions<MovieSpaceContext> options) : base(options) 
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
