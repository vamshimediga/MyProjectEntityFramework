using Microsoft.EntityFrameworkCore;
using MyProjectEntity.Entities;

namespace MyProjectEntity.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
