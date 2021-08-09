using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Password)]
        public string Password { get; set; }

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
    }
}
