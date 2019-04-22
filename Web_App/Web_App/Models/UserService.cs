using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Web_App.Entities;
using Web_App.Helpers;
using Web_App.ViewModels;

namespace Web_App.Models
{
    public interface IUserService
    {
        User GetUser(LoginViewModel login);

        bool CheckUserByUserName(string userName);

        bool CheckUserByUserNamePwd(string userName, string password);

        void CreateUser(UserCreateViewModel userView);
    }
    
    public class UserService : IUserService
    {
        private readonly DBContext _context;

        public UserService(DBContext context)
        {
            _context = context;
        }

        public User GetUser(LoginViewModel login)
        {
            var isAuthentication = _context.Users.Where( u => u.UserName == login.UserName && u.Password == login.Password)
                .Include(x => x.Role)
                .FirstOrDefault();

            return isAuthentication;
        }

        public bool CheckUserByUserName(string userName)
        {
            bool validUser = true;
            var user = _context.Users.Where(u => u.UserName == userName).Select(u => u.UserName).FirstOrDefault();

            if (user != null)
            {
                validUser = false;
            }
            
            return validUser;
        }
        
        public bool CheckUserByUserNamePwd(string userName, string password)
        {
            bool validUser = false;
            var user = _context.Users.Where(u => u.UserName == userName && u.Password == password)
                .Select(u => u.UserName)
                .FirstOrDefault();

            if (user != null)
            {
                validUser = true;
            }
            
            return validUser;
        }
        
        public void CreateUser(UserCreateViewModel userView)
        {
            var user = new User()
            {
                UserName = userView.UserName,
                Password = userView.Password,
                RoleID = userView.RoleId,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}