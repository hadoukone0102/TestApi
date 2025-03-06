using Microsoft.EntityFrameworkCore;

namespace TestApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        // Create a DbSet for the StudentTest model
        public DbSet<Models.StudentTest> StudentTests { get; set; }

        // Create a DbSet for the Cours model
        public DbSet<Models.Cours> Courses { get; set; }
        // Create a DbSet for the User
        public DbSet<Models.User> Users { get; set; }

    }
}
