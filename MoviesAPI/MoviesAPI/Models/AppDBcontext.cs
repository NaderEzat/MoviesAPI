using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class AppDBcontext :DbContext
    {

        public AppDBcontext(DbContextOptions<AppDBcontext>options):base(options)
        {

        }
        public DbSet<Genra> Genras { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
