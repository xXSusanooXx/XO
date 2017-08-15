using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using XO.DAL;
using XO.Model.DbModels;
using XO.Model.DTO;

namespace XO.BLL
{
    public class UsersBll
    {
        private EfContext _context;

        public UsersBll()
        {
            _context = new EfContext();
        }

        public User Get(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public User Get(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsEnumerable();
        }

        public User Add(UserDTO user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Login == user.Login);

            if (dbUser != null) return null;

            var newUser = new User
            {
                Login = user.Login,
                Password = user.Password,
                Role = user.Role
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public bool Remove(int userId)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (dbUser != null)
            {
                _context.Users.Remove(dbUser);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Remove(string login)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Login == login);

            if (dbUser != null)
            {
                _context.Users.Remove(dbUser);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
