using Microsoft.EntityFrameworkCore;

namespace TestApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Models.StudentTest> StudentTests { get; set; }
    }
}
