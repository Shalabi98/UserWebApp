using NetTopologySuite.Geometries;

namespace UserWebApp.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public Point Location { get; set; }

        public Polygon DeliveryAreaCoverage { get; set; }

        public double Distance { get; set; }

        public bool IsWithinDistance { get; set; }

        public bool Within { get; set; }

        public bool Intersects { get; set; }

        public Geometry Intersection { get; set; }

    }
}
