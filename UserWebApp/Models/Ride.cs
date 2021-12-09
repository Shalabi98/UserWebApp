using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }

        [Required]
        [DisplayName("Select Origin and Destination")]
        public double OriginLat { get; set; }

        [Required]
        public double OriginLng { get; set; }

        [Required]
        public double DestinationLat { get; set; }

        [Required]
        public double DestinationLng { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Duration { get; set; }

        [Required]
        public double Price { get; set; }
        
        public string Currency { get; set; } = "JOD";

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
