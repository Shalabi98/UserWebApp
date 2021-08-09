using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserWebApp.IServices;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UniversityContext _context;

        public APIController(IUserService userService, UniversityContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        [Route("GetUser")]
        public ActionResult GetUser()
        {
            if (ModelState.IsValid)
            {
                var users = _userService.GetUser();
                return Ok(new { Result = "OK", Users = users });
            }
            else
            {
                return NotFound(new { Result = "USER_RECORDS_NOT_FOUND" });
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser(User user) 
        {
            if (ModelState.IsValid)
            {
                var isUserExist = _context.User.Any(s => s.UserName.Equals(user.UserName));
                var isEmailExist = _context.User.Any(s => s.Email.Equals(user.Email));
                var isPhoneExist = _context.User.Any(s => s.MobilePhoneNo.Equals(user.MobilePhoneNo));

                if (!isUserExist && !isEmailExist && !isPhoneExist)
                {
                    _userService.CreateUser(user);
                    _context.SaveChanges();
                    return Ok(new { Result = "SUCCESS_USER_CREATED" });
                }
                if (isUserExist)
                {
                    return BadRequest(new { Result = "ERROR_USER_RECORD_EXISTS" });
                }
                if (isEmailExist)
                {
                    return BadRequest(new { Result = "ERROR_EMAIL_ADDRESS_EXISTS" });
                }
                if (isPhoneExist)
                {
                    return BadRequest(new { Result = "ERROR_PHONE_NUMBER_EXISTS" });
                }
            }
            return Ok();
        }
    }
}
