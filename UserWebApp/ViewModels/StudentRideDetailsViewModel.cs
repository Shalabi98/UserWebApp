using System.Collections.Generic;

namespace UserWebApp.ViewModels
{
    public class StudentRideDetailsViewModel
    {
        public IEnumerable<RideViewModel> Rides { get; set; }
        
        public string TotalPrice { get; set; }

        public int StudentId { get; set; }
    }
}
