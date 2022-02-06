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

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
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
                        new Restaurant() {RestaurantId = 1, Name = "Al Quds", City = "Amman", Location = geometryFactory.CreatePoint(new Coordinate(35.9323535711242, 31.95302249260005)),
                            DeliveryAreaCoverage = geometryFactory.CreatePolygon(new Coordinate[]
                            {
                                new Coordinate (35.944914401626164, 32.05746209539408),
                                new Coordinate (36.05840058444662, 31.99578659548469),
                                new Coordinate (36.06422038869383, 31.904430696264882),
                                new Coordinate (35.87798665278333, 31.779595771940045),
                                new Coordinate (35.75140591040666, 31.805565454728104),
                                new Coordinate (35.67720340625483, 32.00565746582009),
                                new Coordinate (35.944914401626164, 32.05746209539408)
                            })
                        },
                        new Restaurant() {RestaurantId = 2, Name = "Hanini", City = "Amman", Location = geometryFactory.CreatePoint(new Coordinate(35.95048433946144, 31.974175313038348)), 
                            DeliveryAreaCoverage = geometryFactory.CreatePolygon(new Coordinate[]
                            {
                                new Coordinate (35.944914401626164, 32.05746209539408),
                                new Coordinate (36.05840058444662, 31.99578659548469),
                                new Coordinate (36.06422038869383, 31.904430696264882),
                                new Coordinate (35.87798665278333, 31.779595771940045),
                                new Coordinate (35.75140591040666, 31.805565454728104),
                                new Coordinate (35.67720340625483, 32.00565746582009),
                                new Coordinate (35.944914401626164, 32.05746209539408)
                            })
                        },
                        new Restaurant() {RestaurantId = 3, Name = "Barcelona", City = "Amman", Location = geometryFactory.CreatePoint(new Coordinate(35.86016216507191, 31.89156322955307)),
                            DeliveryAreaCoverage = geometryFactory.CreatePolygon(new Coordinate[]
                            {
                                new Coordinate (35.944914401626164, 32.05746209539408),
                                new Coordinate (36.05840058444662, 31.99578659548469),
                                new Coordinate (36.06422038869383, 31.904430696264882),
                                new Coordinate (35.87798665278333, 31.779595771940045),
                                new Coordinate (35.75140591040666, 31.805565454728104),
                                new Coordinate (35.67720340625483, 32.00565746582009),
                                new Coordinate (35.944914401626164, 32.05746209539408)
                            })
                        },
                        new Restaurant() {RestaurantId = 4, Name = "Tazaj", City = "Amman", Location = geometryFactory.CreatePoint(new Coordinate(35.886529536501136, 31.984122756596005)), 
                            DeliveryAreaCoverage = geometryFactory.CreatePolygon(new Coordinate[]
                            {
                                new Coordinate (35.944914401626164, 32.05746209539408),
                                new Coordinate (36.05840058444662, 31.99578659548469),
                                new Coordinate (36.06422038869383, 31.904430696264882),
                                new Coordinate (35.87798665278333, 31.779595771940045),
                                new Coordinate (35.75140591040666, 31.805565454728104),
                                new Coordinate (35.67720340625483, 32.00565746582009),
                                new Coordinate (35.944914401626164, 32.05746209539408)
                            })
                        },
                        new Restaurant() {RestaurantId = 5, Name = "KFC", City = "Amman", Location= geometryFactory.CreatePoint(new Coordinate(35.854908281588884, 31.95462276640964)),
                            DeliveryAreaCoverage = geometryFactory.CreatePolygon(new Coordinate[]
                            {
                                new Coordinate (35.944914401626164, 32.05746209539408),
                                new Coordinate (36.05840058444662, 31.99578659548469),
                                new Coordinate (36.06422038869383, 31.904430696264882),
                                new Coordinate (35.87798665278333, 31.779595771940045),
                                new Coordinate (35.75140591040666, 31.805565454728104),
                                new Coordinate (35.67720340625483, 32.00565746582009),
                                new Coordinate (35.944914401626164, 32.05746209539408)
                            })
                        },
                        new Restaurant() {RestaurantId = 6, Name = "SoYummy", City = "Aqaba", Location= geometryFactory.CreatePoint(new Coordinate(35.00316439395086, 29.54277244939043)),
                            DeliveryAreaCoverage = geometryFactory.CreatePolygon(new Coordinate[]
                            {
                                new Coordinate (35.944914401626164, 32.05746209539408),
                                new Coordinate (36.05840058444662, 31.99578659548469),
                                new Coordinate (36.06422038869383, 31.904430696264882),
                                new Coordinate (35.87798665278333, 31.779595771940045),
                                new Coordinate (35.75140591040666, 31.805565454728104),
                                new Coordinate (35.67720340625483, 32.00565746582009),
                                new Coordinate (35.944914401626164, 32.05746209539408)
                            })
                        }
                    });
        }

        public DbSet<User> User { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Streaming> Streams { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
