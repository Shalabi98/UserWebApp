using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebApp.IServices;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityContext db;
        private readonly IBlobService _blobService;

        public StudentsController(UniversityContext context, IBlobService blobService)
        {
            this.db = context;
            _blobService = blobService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = db.Students.ToList();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([Bind("StudentId,Username,FirstName,LastName,EnrollmentDate,Gender,DateOfBirth,Email," +
            "MobilePhoneNo","City","Address","Image","UploadedImage","IsGraduating")] Student student)
        {
            if (ModelState.IsValid)
            {
                var isStudentExist = db.Students.Any(s => s.Username.Equals(student.Username));
                var isEmailExist = db.Students.Any(s => s.Email.Equals(student.Email));
                var isPhoneExist = db.Students.Any(s => s.MobilePhoneNo.Equals(student.MobilePhoneNo));

                var extention = student.UploadedImage.FileName.Split('.').Last();
                var uploadResult = await _blobService.UploadBlob($"{Guid.NewGuid()}.{extention}", student.UploadedImage);

                if (!isStudentExist && !isEmailExist && !isPhoneExist)
                {
                    var fileName = student.UploadedImage.FileName;
                    student.Image = fileName;
                    db.Add(student);
                    db.SaveChanges();

                    ViewBag.StudentMessage = "Student Record Successfully Created";

                    return RedirectToAction(nameof(Index));
                }
                if (isStudentExist)
                {
                    ViewBag.StudentMessage = "Student Username Exists, Try Again!";
                }
                if (isEmailExist)
                {
                    ViewBag.StudentMessage = "Email Exists, Try Again!";

                }
                if (isPhoneExist)
                {
                    ViewBag.StudentMessage = "Phone Number Exists, Try Again!";
                }
            }
            GetGenderDropDown();
            return View(student);
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            GetGenderDropDown();
            return View(new Student());
        }

        [HttpPost]
        public IActionResult EditStudent([Bind("StudentId,Username,FirstName,LastName,EnrollmentDate,Gender,DateOfBirth,Email," +
            "MobilePhoneNo","City","Address","UploadedImage","IsGraduating")] Student student)
        {
            GetGenderDropDown();
            if (ModelState.IsValid)
            {
                var isEmailExist = db.Students.Any(s => s.Email.Equals(student.Email) && s.StudentId != student.StudentId);
                var isPhoneExist = db.Students.Any(s => s.MobilePhoneNo.Equals(student.MobilePhoneNo) && s.StudentId != student.StudentId);
                if (!isEmailExist && !isPhoneExist)
                {
                    db.Update(student);
                    db.SaveChanges();

                    ViewBag.StudentEditMessage = "Student Record Successfully Edited";

                    return View(student);
                }
                if (isEmailExist)
                {
                    ViewBag.StudentEditMessage = "Email Exists, Try Again!";
                }
                if (isPhoneExist)
                {
                    ViewBag.StudentEditMessage = "Phone Number Exists, Try Again!";
                }
            }
            return View(new Student());
        }

        [HttpGet]
        public IActionResult EditStudent(int id = 0)
        {
            if (id == 0)
            {
                return View(new Student());
            }
            else
            {
                GetGenderDropDown();
                return View(db.Students.Find(id));
            }
        }

        [HttpGet]
        public void GetGenderDropDown()
        {
            var gender = new List<SelectListItem>
                {
                    new SelectListItem { Text="Male", Value = "1"},
                    new SelectListItem { Text="Female", Value = "2"},
                }.AsEnumerable();
            ViewBag.GenderList = gender;
        }

        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {
            var isExists = db.Enrollments.Any(e => e.StudentId == id);
            if (!isExists)
            {
                var removeStudent = db.Students.Find(id);
                db.Students.Remove(removeStudent);
                db.SaveChanges();
                TempData["StudentDeleteMessage"] = "Student Record Deleted Successfully.";
            }
            else
            {
                TempData["StudentDeleteMessage"] = "This Student is Enrolled in a Course, Failed to Delete!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
