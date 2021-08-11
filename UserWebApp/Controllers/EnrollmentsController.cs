using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web.Http.Controllers;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    
    [Authorize]
    public class EnrollmentsController : Controller
    {
        private readonly UniversityContext db;
        public EnrollmentsController(UniversityContext context )
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.Email);

            var enrollments = db.Enrollments.ToList();
            GetStudentName();
            GetCourseTitle();
            return View(enrollments);
        }

        [HttpPost]
        public IActionResult CreateEnrollment([Bind("EnrollmentId,StudentId,CourseId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                var isEnrollmentExist = db.Enrollments.Any(e => e.StudentId.Equals(enrollment.StudentId)
                        && e.CourseId == enrollment.CourseId);

                if (!isEnrollmentExist)
                {
                    db.Add(enrollment);
                    db.SaveChanges();
                    ViewBag.EnrollmentMessage = "Enrollment Record Successfully Created";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.EnrollmentMessage = "Enrollment Record Exists!";
                }
            }

            GetStudentName();
            GetCourseTitle();

            return View(enrollment);
        }

        [HttpGet]
        public IActionResult CreateEnrollment()
        {
            GetStudentName();
            GetCourseTitle();

            return View(new Enrollment());
        }

        public IActionResult EditEnrollment([Bind("EnrollmentId,StudentId,CourseId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Update(enrollment);
                db.SaveChanges();
                ViewBag.EnrollmentEditMessage = "Enrollment Record Successfully Edited";

                return View(enrollment);
            }
            else
            {
                ViewBag.EnrollmentEditMessage = "An Error Occured, Try Again.";
            }

            GetStudentName();
            GetCourseTitle();

            return View(new Enrollment());
        }

        [HttpGet]
        public IActionResult EditEnrollment(int id = 0)
        {
            if (id == 0)
            {
                return View(new Enrollment());
            }
            else
            {
                GetStudentName();
                GetCourseTitle();
                return View(db.Enrollments.Find(id));
            }
        }

        [HttpGet]
        public IActionResult DeleteEnrollment(int id)
        {
            var enroll = db.Enrollments.Find(id);
            db.Enrollments.Remove(enroll);
            db.SaveChanges();
            TempData["EnrollmentDeleteMessage"] = "Enrollment Record Deleted Successfully.";

            return RedirectToAction(nameof(Index));
        }

        public void GetStudentName()
        {
            var student = db.Students.ToList();
            var students = student.Select(e => new SelectListItem
            {
                Text = e.FirstName,
                Value = e.StudentId.ToString()
            });

            ViewBag.StudentName = students;
        }

        public void GetCourseTitle()
        {
            var course = db.Courses.ToList();
            var courses = course.Select(e => new SelectListItem
            {
                Text = e.Title,
                Value = e.CourseId.ToString()
            });

            ViewBag.CourseTitle = courses;
        }
    }
}
