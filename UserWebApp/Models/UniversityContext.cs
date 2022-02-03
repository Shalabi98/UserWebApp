using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
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

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            modelBuilder.Entity<Restaurant>()
                .HasData(
                    new List<Restaurant>()
                    {
                        new Restaurant() {RestaurantId = 1, Name = "Chapatti", City = "Amman", Location = geometryFactory.CreatePoint(new Coordinate(35.86401397105961, 31.956700034429126))},
                        new Restaurant() {RestaurantId = 2, Name = "Hamam", City = "Zarqa", Location = geometryFactory.CreatePoint(new Coordinate(36.089075941349364, 32.065718789490624))},
                        new Restaurant() {RestaurantId = 3, Name = "So Yummy", City = "Aqaba", Location = geometryFactory.CreatePoint(new Coordinate(35.00314101505938, 29.542843619043456))},
                        new Restaurant() {RestaurantId = 4, Name = "Sultan", City = "Irbid", Location = geometryFactory.CreatePoint(new Coordinate(35.86709690468255, 32.55783701513558))}
                    });
        }   

        public DbSet<User> User { get; set; }
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Streaming> Streams { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
