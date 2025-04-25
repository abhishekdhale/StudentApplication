using Microsoft.EntityFrameworkCore;
using BasicApplication.Models;

namespace BasicApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }

        public void SeedData()
        {
            try
            {
                // Seed Courses
                if (!Courses.Any(c => c.Id == 1))
                {
                    Courses.Add(new Course { Id = 1, Name = "Introduction to Programming", Description = "Basic programming concepts", CreatedAt = DateTime.Parse("2024-01-01") });
                }
                if (!Courses.Any(c => c.Id == 2))
                {
                    Courses.Add(new Course { Id = 2, Name = "Web Development", Description = "Building web applications", CreatedAt = DateTime.Parse("2024-01-01") });
                }
                if (!Courses.Any(c => c.Id == 3))
                {
                    Courses.Add(new Course { Id = 3, Name = "Database Management", Description = "SQL and database design", CreatedAt = DateTime.Parse("2024-01-01") });
                }

                var adminUser = Users.FirstOrDefault(u => u.Username == "admin");
                // Seed Admin User
                if (adminUser == null)
                {
                    
                    
                        Users.Add(new User
                        {
                            Id = 1,
                            Username = "admin",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                            CreatedAt = DateTime.Parse("2024-01-01")
                        });
                    
                }
                

                SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }
}