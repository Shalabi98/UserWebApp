using Microsoft.AspNetCore.Mvc;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Linq;
using UserWebApp.Extensions;
using ProjNet;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly UniversityContext db;

        public RestaurantsController(UniversityContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            //The following is an implementation using NetTopologySuites
            var referenceLocation = new Point(35.909652916322976, 31.97296214755054) { SRID = 4326 };
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //Points
            var amman = new Point(35.86401397105961, 31.956700034429126) { SRID = 4326 };
            var aqaba = new Point(35.00314101505938, 29.542843619043456) { SRID = 4326 };

            //Validate SRID
            if (!amman.isValidSrid() || !aqaba.isValidSrid())
            {
                return View();
            }

            //Return Distance between two Points in degrees (SRID 4326 default) and meters (ProjNet -> convert to 2855)
            var distanceInDegrees = amman.Distance(aqaba);
            var distanceInMeters = amman.ProjectTo(2855).Distance(aqaba.ProjectTo(2855));
            //Check if two points intersect with one another
            bool isPointsIntersected = amman.Intersects(aqaba);

            //Polygons 
            var polygon1 = geometryFactory.CreatePolygon(new Coordinate[] 
            { 
                new Coordinate { X = 31.973188517904152, Y = 35.90957636245783 }, 
                new Coordinate { X = 31.953957109594732, Y = 35.91048673434372 },
                new Coordinate { X = 31.963957109594732, Y = 35.92048673434372 },
                new Coordinate { X = 31.973188517904152, Y = 35.90957636245783 }
            });
            //Set the SRID of the Polygon for Standardization
            polygon1.SRID = 4326;

            //To Validate Coordinates of Polygon -> A Polygon must have the same first and last Coordinates Values.
            if (polygon1.Coordinates.First().CoordinateValue.ToString() != polygon1.Coordinates.Last().CoordinateValue.ToString())
            {
                return View();
            }

            var polygon2 = geometryFactory.CreatePolygon(new Coordinate[]
            {
                new Coordinate { X = 31.973188517904152, Y = 35.90957636245783 },
                new Coordinate { X = 31.952000274720252, Y = 35.899531925983474 },
                new Coordinate { X = 31.962000274720252, Y = 35.889531925983474 },
                new Coordinate { X = 31.973188517904152, Y = 35.90957636245783 }
            });
            polygon2.SRID = 4326;
            if (polygon2.Coordinates.First().CoordinateValue.ToString() != polygon2.Coordinates.Last().CoordinateValue.ToString())
            {
                return View();
            }

            var polygon3 = geometryFactory.CreatePolygon(new Coordinate[]
            {
                new Coordinate { X = 39.095395129109576, Y = -94.57007918587617 },
                new Coordinate { X = 39.09515515172112, Y = -94.56463488141775 },
                new Coordinate { X = 39.091212549133196, Y = -94.56485574569597 },
                new Coordinate { X = 39.095395129109576, Y = -94.57007918587617 }
            });
            polygon3.SRID = 4326;
            if (polygon3.Coordinates.First().CoordinateValue.ToString() != polygon3.Coordinates.Last().CoordinateValue.ToString())
            {
                return View();
            }

            var polygon4 = geometryFactory.CreatePolygon(new Coordinate[]
            {
                new Coordinate { X = 39.089935441467844, Y = -94.56768280845736 },
                new Coordinate { X = 39.08818688067001, Y = -94.56782637018232 },
                new Coordinate { X = 39.089832586107974, Y = -94.564491319581 },
                new Coordinate { X = 39.089935441467844, Y = -94.56768280845736 }
            });
            polygon4.SRID = 4326;
            if (polygon4.Coordinates.First().CoordinateValue.ToString() != polygon4.Coordinates.Last().CoordinateValue.ToString())
            {
                return View();
            }

            //Return bool to check if two polygons intersect one another
            var isPolygonsIntersected = polygon1.Intersects(polygon2);
            //Return intersected polygon object 
            var polygonIntersection = polygon1.Intersection(polygon2);
            //Check geometry type of the intersect 
            var check = polygonIntersection.GeometryType;

            //MultiPolygon 
            //var multiPolygon1 = new MultiPolygon(new Polygon[] { polygon1, polygon2});
            //var multiPolygon2 = new MultiPolygon(new Polygon[] { polygon3, polygon4});
            var multiPolygon1 = geometryFactory.CreateMultiPolygon(new Polygon[] { polygon1, polygon2 });
            multiPolygon1.SRID = 2855;
            var multiPolygon2 = geometryFactory.CreateMultiPolygon(new Polygon[] { polygon3, polygon4 });
            multiPolygon2.SRID = 4326;


            //Validate Multipolygon Operation
            var isValidOp1 = new NetTopologySuite.Operation.Valid.IsValidOp(multiPolygon1);
            var isValidOp2 = new NetTopologySuite.Operation.Valid.IsValidOp(multiPolygon2);
            if (!isValidOp1.IsValid || !isValidOp2.IsValid)
            {
                var err1 = isValidOp1.ValidationError;
                var err2 = isValidOp2.ValidationError;
                return View();
            }

            //Return bool to check if two multipolygons intersect one another
            var isMultiPolygonsIntersected = multiPolygon1.Intersects(multiPolygon2);

            //Create a Reference Polygon
            var ammanPolygon = geometryFactory.CreatePolygon(new Coordinate[]
            {
                new Coordinate { X = 32.05746209539408, Y = 35.944914401626164 },
                new Coordinate { X = 31.99578659548469, Y = 36.05840058444662 },
                new Coordinate { X = 31.904430696264882, Y = 36.06422038869383 },
                new Coordinate { X = 31.779595771940045, Y = 35.87798665278333 },
                new Coordinate { X = 31.805565454728104, Y = 35.75140591040666 },
                new Coordinate { X = 32.00565746582009, Y = 35.67720340625483 },
                new Coordinate { X = 32.05746209539408, Y = 35.944914401626164 }
            });

            //Check if a Geometry object exists within another Geometry object
            var isWithinAmman = polygon1.Within(ammanPolygon);

            var courses = db.Restaurants.OrderBy(r => r.Location.Distance(referenceLocation))
                .Select(r => new Restaurant
                {
                    Name = r.Name,
                    City = r.City,
                    Distance = r.Location.Distance(referenceLocation),
                    IsWithinDistance = r.Location.IsWithinDistance(referenceLocation, 20000)
                }).ToList();
            return View(courses);
        }
    }
}
