
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

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<AdminSignup>().ToTable("adminsignup");
            modelBuilder.Entity<AdminLogin>().ToTable("adminlogin");
            modelBuilder.Entity<StudentSignup>().ToTable("studentsignup");
            modelBuilder.Entity<StudentLogin>().ToTable("studentlogin");
        }
    }
}
