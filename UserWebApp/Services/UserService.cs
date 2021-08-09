using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebApp.IServices;
using UserWebApp.Models;

namespace UserWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly UniversityContext _context;

        public UserService(UniversityContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            if(user != null)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public IEnumerable<User> GetUser()
        {
            var users = _context.User.ToList();
            return users;
        }
    }
}
