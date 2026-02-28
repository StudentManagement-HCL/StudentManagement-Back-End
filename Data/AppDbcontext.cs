using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AdminSignup> AdminSignup { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }
        public DbSet<StudentSignup> StudentSignup { get; set; }
        public DbSet<StudentLogin> StudentLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminSignup>().ToTable("AdminSignup");
            modelBuilder.Entity<AdminLogin>().ToTable("AdminLogin");
            modelBuilder.Entity<StudentSignup>().ToTable("StudentSignup");
            modelBuilder.Entity<StudentLogin>().ToTable("StudentLogin");
        }
    }
}
