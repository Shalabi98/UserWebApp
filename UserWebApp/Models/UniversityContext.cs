using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserWebApp.Models
{
    public class UniversityContext : IdentityDbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    SqlConnectionStringBuilder ConnectionString = new SqlConnectionStringBuilder()
        //    {
        //        DataSource = I - SHALABI,
        //        InitialCatalog = UserDB,
        //        IntegratedSecurity = true;
        //};

        //optionsBuilder.UseSqlServer(ConnectionString.ToString());
        //}
    }
}
