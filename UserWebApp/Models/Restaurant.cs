using NetTopologySuite.Geometries;

namespace UserWebApp.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public Point Location { get; set; }

        public double Distance { get; set; }

        public bool IsWithinDistance { get; set; }
    }
}
