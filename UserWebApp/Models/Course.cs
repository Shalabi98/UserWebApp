using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Course Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Course Code")]
        [StringLength(5, MinimumLength = 2)]
        public string Code { get; set; }

        [Required]
        [DisplayName("Credit Hours")]
        [Range(1, 8)]
        public int CreditHours { get; set; }

        [Required]
        [DisplayName("Semester")]
        [StringLength(6, MinimumLength = 2)]
        public string Semester { get; set; }

        [Required]
        [DisplayName("Location")]
        [StringLength(15, MinimumLength = 2)]
        public string Location { get; set; }

        public List<Enrollment> Enrollments { get; set; }
    }
}
