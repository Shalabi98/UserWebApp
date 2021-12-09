using Cultures.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserWebApp.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Username")]
        [StringLength(30, MinimumLength = 8)]
        public string UserName { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Gender")]
        public Gender Gender { get; set; }

        [Required]
        [DisplayName("Age")]
        [Range(1, 100)]
        public int Age { get; set; }

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

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Location")]
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required]
        [DisplayName("Time Zone")]
        public string TimeZoneId { get; set; }

        [Required]
        [DisplayName("Culture")]
        public string Culture { get; set; }

        [Required]
        [DisplayName("Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [NotMapped]
        [DisplayName("Registration Date")]
        public string DisplayRegistrationDate
        {
            get
            {
                return RegistrationDate.ToLocalTime(TimeZoneId).ToFormat(Culture);
            }
        }
    }
}
