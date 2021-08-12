using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using UserWebApp.Models;

namespace UserWebApp.Hubs.Common
{
    public abstract class MainHub : Hub
    {
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

                _applicationUser.Email = Context.User.FindFirstValue(ClaimTypes.Email);
                _applicationUser.FirstName = Context.User.FindFirstValue("FirstName");
                _applicationUser.LastName = Context.User.FindFirstValue("LastName");

                return _applicationUser;
            }
        }
    }
}
