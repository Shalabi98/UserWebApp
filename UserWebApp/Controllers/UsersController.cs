﻿using Cultures.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UniversityContext db;
        public UsersController(UniversityContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser([Bind("UserId,UserName,FirstName,LastName,Gender,Age,Email,MobilePhoneNo,IsActive,Latitude,Longitude,TimeZoneId,Culture")] User user)
        {
            if (ModelState.IsValid)
            {
                user.RegistrationDate = DateTime.UtcNow;
                var isUserExist = db.User.Any(s => s.UserName.Equals(user.UserName));
                var isEmailExist = db.User.Any(s => s.Email.Equals(user.Email));
                var isPhoneExist = db.User.Any(s => s.MobilePhoneNo.Equals(user.MobilePhoneNo));
                if (!isUserExist && !isEmailExist && !isPhoneExist)
                {
                    db.Add(user);
                    db.SaveChanges();
                    ViewBag.UserMessage = "User Record Successfully Created";

                    return RedirectToAction(nameof(Index));
                }
                if (isUserExist)
                {
                    ViewBag.UserMessage = "Username Exists, Try Again!";
                }
                if (isEmailExist)
                {
                    ViewBag.UserMessage = "Email Exists, Try Again!";

                }
                if (isPhoneExist)
                {
                    ViewBag.UserMessage = "Phone Number Exists, Try Again!";

                }
            }
            GetGenderDropDown();
            GetTimeZonesId();
            GetCultures();

            return View(user);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            GetGenderDropDown();
            GetTimeZonesId();
            GetCultures();

            return View(new User());
        }

        [HttpPost]
        public IActionResult EditUser([Bind("UserId,UserName,FirstName,LastName,Gender,Age,Email,MobilePhoneNo,IsActive,Latitude,Longitude,TimeZoneId,Culture,RegistrationDate")] User user)
        {
            GetGenderDropDown();
            GetTimeZonesId();
            GetCultures();

            if (ModelState.IsValid)
            {
                var isEmailExist = db.User.Any(s => s.Email.Equals(user.Email) && s.UserId != user.UserId);
                var isPhoneExist = db.User.Any(s => s.MobilePhoneNo.Equals(user.MobilePhoneNo) && s.UserId != user.UserId);
                if (!isEmailExist && !isPhoneExist)
                {
                    db.Update(user);
                    db.SaveChanges();
                    ViewBag.UserEditMessage = "User Record Successfully Edited";

                    return View(user);
                }
                if (isEmailExist)
                {
                    ViewBag.UserEditMessage = "Email Exists, Try Again!";
                }
                if (isPhoneExist)
                {
                    ViewBag.UserEditMessage = "Phone Number Exists, Try Again!";
                }
            }
            return View(new User());
        }

        [HttpGet]
        public IActionResult EditUser(int id = 0)
        {
            if (id == 0)
            {
                return View(new User());
            }
            else
            {
                GetGenderDropDown();
                GetTimeZonesId();
                GetCultures();
                return View(db.User.Find(id));
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            TempData["UserDeleteMessage"] = "User Record Deleted Successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetSearchData(string SearchValue)
        {
            List<User> users = new List<User>();
            users = db.User.Where(u => u.UserName.Contains(SearchValue) || SearchValue == null).ToList();
            return PartialView("_Users", users);
        }

        [HttpGet]
        public void GetGenderDropDown()
        {
            var gender = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Male", Value = "1"},
                    new SelectListItem { Text = "Female", Value = "2"},
                }.AsEnumerable();
            ViewBag.GenderList = gender;
        }

        [HttpGet]
        public void GetTimeZonesId()
        {
            ViewBag.TimeZoneList = new SelectList(TimeZoneInfo.GetSystemTimeZones(), "Id", "DisplayName");
        }

        [HttpGet]
        public void GetCultures()
        {
            ViewBag.CultureList = new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures), "Name", "NativeName");
        }
    }
}
