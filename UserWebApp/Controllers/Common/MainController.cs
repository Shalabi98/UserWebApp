using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserWebApp.Models;

namespace UserWebApp.Controllers.Common
{
    public abstract class MainController : Controller
    {
        public MainController()
        {
        }

        private ApplicationUser _applicationUser;

        public ApplicationUser ApplicationUser
        {
            get
            {
                if (_applicationUser != null)
                {
                    return _applicationUser;
                }

                _applicationUser = new ApplicationUser();

                _applicationUser.Email = User.FindFirstValue(ClaimTypes.Email);
                _applicationUser.FirstName = User.FindFirstValue("FirstName");
                _applicationUser.LastName = User.FindFirstValue("LastName");

                return _applicationUser;
            }
        }
    }
}
