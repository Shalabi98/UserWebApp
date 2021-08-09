using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebApp.Models;

namespace UserWebApp.IServices
{
    public interface IUserService
    {
        User CreateUser(User user);
        IEnumerable<User> GetUser();
    }
}
