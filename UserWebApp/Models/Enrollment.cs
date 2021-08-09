using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage = "Course Title Field is Required.")]

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Student Name Field is Required.")]
        public int StudentId { get; set; }
        
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
