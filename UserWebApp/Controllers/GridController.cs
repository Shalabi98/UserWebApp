using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UserWebApp.Models;
using UserWebApp.ViewModels;

namespace UserWebApp.Controllers
{
    public class GridController : Controller
    {
        private readonly UniversityContext _dbContext;
        public GridController(UniversityContext context)
        {
            _dbContext = context;
        }

        public IActionResult StudentsGrid()
        {
            return View();
        }

        public IActionResult GetAllStudents([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<StudentViewModel> students = _dbContext.Students.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                Username = s.Username,
                FirstName = s.FirstName,
                LastName = s.LastName,
                EnrollmentDate = s.EnrollmentDate.ToString(),
                Gender = s.Gender.ToString(),
                DateOfBirth = s.DateOfBirth.ToString(),
                Email = s.Email,
                MobilePhoneNo = s.MobilePhoneNo,
                City = s.City,
                Address = s.Address,
                IsGraduating = s.IsGraduating
            }).ToList();
           
            return Json(students.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}
