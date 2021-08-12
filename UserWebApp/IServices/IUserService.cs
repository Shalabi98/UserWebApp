using System.Collections.Generic;
using UserWebApp.Models;

namespace UserWebApp.IServices
{
    public interface IUserService
    {
        User CreateUser(User user);
        IEnumerable<User> GetUser();
    }
}
