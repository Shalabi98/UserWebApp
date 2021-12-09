using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserWebApp.Models;
using System.Linq;
using UserWebApp.ViewModels;
using System.Collections.Generic;

namespace UserWebApp.Controllers
{
    [Authorize]
    public class RidesController : Controller
    {
        private readonly UniversityContext _dbContext;

        public RidesController(UniversityContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            return View(new Ride() { StudentId = id });
        }

        [HttpPost]
        public IActionResult CreateRideRequest([Bind("RideId,OriginLat,OriginLng,DestinationLat,DestinationLng,Distance,Duration,Price,Currency,StudentId")] Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Result = "BAD_REQUEST" });
            }

            _dbContext.Add(ride);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

        [HttpGet]
        public IActionResult RideDetails(int id)
        {
            ViewData["StudentId"] = id;

            return View();
        }

        [HttpPost]
        public IActionResult SortData(string sortName, string sortOrder, int id)
        {
            var dbRides = _dbContext.Rides.Where(s => s.StudentId.Equals(id));

            switch (sortName)
            {
                case "Price":
                    if (sortOrder == "ASC")
                    {
                        dbRides = dbRides.OrderBy(s => s.Price);
                    }
                    else
                    {
                        dbRides = dbRides.OrderByDescending(s => s.Price);
                    }
                break;
            }

            return PartialView("_Rides", ToViewModel(dbRides, id));
        }

        private StudentRideDetailsViewModel ToViewModel(IEnumerable<Ride> rides, int studentId)
        {
            var model = new StudentRideDetailsViewModel();
            model.TotalPrice = $"Total Price: {rides.Sum(r => r.Price):#.##} JOD";
            model.StudentId = studentId;
            model.Rides = rides.Select(r => new RideViewModel()
            {
                Distance = $"{r.Distance / 1000:#.##} km",
                Duration = $"{r.Duration / 60:#.##} min",
                Price = $"{r.Price:#.##} {r.Currency}",
            }).ToList();

            return model;
        }
    }
}
