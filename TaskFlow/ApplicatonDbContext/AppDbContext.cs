using Microsoft.EntityFrameworkCore;
using TaskFlow.Entites;

namespace TaskFlow.ApplicatonDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
