using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserWebApp.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First Name")]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
    }
}