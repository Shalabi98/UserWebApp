using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserWebApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [DisplayName("Username")]
        [StringLength(30, MinimumLength = 8)]
        public string Username { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Enrollment Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        [DisplayName("Gender")]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        [RegularExpression(@"\b[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^\+[1-9]{1}[0-9]{3,14}$")]
        public string MobilePhoneNo { get; set; }

        [DisplayName("City")]
        [StringLength(30, MinimumLength = 2)]
        public string City { get; set; }

        [DisplayName("Address")]
        [StringLength(100, MinimumLength = 5)]
        public string Address { get; set; }

        [DisplayName("Upload Image")]
        public string Image { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile UploadedImage { get; set; } 

        [DisplayName("Is Graduating")]
        public bool IsGraduating { get; set; }

        public List<Enrollment> Enrollments { get; set; }

        public ICollection<Ride> Rides { get; set; }
    }
}
