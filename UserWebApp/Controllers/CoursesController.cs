﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using UserWebApp.Hubs;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly UniversityContext db;
        private readonly IHubContext<ChatHub> _hub;
        public CoursesController(UniversityContext context, IHubContext<ChatHub> hub)
        {
            this.db = context;
            _hub = hub;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var courses = db.Courses.ToList();
            return View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([Bind("CourseId,Title,Code,CreditHours,Semester,Location")] Course course)
        {
            if (ModelState.IsValid)
            {
                var isTitleExist = db.Courses.Any(c => c.Title.Equals(course.Title));
                var isCodeExist = db.Courses.Any(c => c.Code.Equals(course.Code));

                if (!isTitleExist && !isCodeExist)
                {
                    db.Add(course);
                    db.SaveChanges();
                    ViewBag.CourseMessage = "Course Record Successfully Created";

                    await _hub.Clients.All.SendAsync("Notification");
                    return View(course);
                }
                if (isTitleExist)
                {
                    ViewBag.CourseMessage = "Course Title Exists, Try Again!";
                }
                if (isCodeExist)
                {
                    ViewBag.CourseMessage = "Course Code Exists, Try Again!";
                }
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View(new Course());
        }

        [HttpPost]
        public IActionResult EditCourse([Bind("CourseId,Title,Code,CreditHours,Semester,Location")] Course course)
        {
            if (ModelState.IsValid)
            {
                var isTitleExist = db.Courses.Any(c => c.Title.Equals(course.Title) && c.CourseId != course.CourseId);
                var isCodeExist = db.Courses.Any(c => c.Code.Equals(course.Code) && c.CourseId != course.CourseId);
                if (!isTitleExist && !isCodeExist)
                {
                    db.Update(course);
                    db.SaveChanges();
                    ViewBag.CourseEditMessage = "Course Record Successfully Edited";

                    return View(course);
                }
                if (isTitleExist)
                {
                    ViewBag.CourseEditMessage = "Course Title Exists, Try Again!";
                }
                if (isCodeExist)
                {
                    ViewBag.CourseEditMessage = "Course Code Exists, Try Again!";
                }
            }
            return View(new Course());
        }

        [HttpGet]
        public IActionResult EditCourse(int id = 0)
        {
            if (id == 0)
            {
                return View(new Course());
            }
            else
            {
                return View(db.Courses.Find(id));
            }
        }

        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            var isExists = db.Enrollments.Any(e => e.CourseId == id);
            if (!isExists)
            {
                var removeCourse = db.Courses.Find(id);
                db.Courses.Remove(removeCourse);
                db.SaveChanges();
                TempData["CourseDeleteMessage"] = "Course Record Deleted Successfully.";
            }
            else
            {
                TempData["CourseDeleteMessage"] = "There are Students Enrolled in this Course, Failed to Delete!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
