using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UserWebApp.Models
{
    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Enrollment>()
                .HasOne(c => c.Course)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(ci => ci.CourseId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(s => s.Student)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(si => si.StudentId).OnDelete(DeleteBehavior.NoAction);
        }   

        public DbSet<User> User { get; set; }
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Streaming> Streams { get; set; }
    }
}
