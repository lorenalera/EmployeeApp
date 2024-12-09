using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Project> Project { get; set; }
        // DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key configuration for UserProject
            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId }); // Define composite primary key

            // Configure the relationships
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.userProjects)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.userProjects)
                .HasForeignKey(up => up.ProjectId);
        }

    }

}

