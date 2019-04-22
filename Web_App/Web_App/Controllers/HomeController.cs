using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web_App.Models;
using Web_App.Helpers;
using System.Security.Claims;
using Web_App.ViewModels;

namespace Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;
        private readonly IUserService _userService;
        private readonly IVacancyService _vacancyService;
        private readonly IRoleService _roleService;

        public HomeController(
            DBContext context, 
            IUserService userService, 
            IVacancyService vacancyService, 
            IRoleService roleService
            )
        {
            _context = context;
            _userService = userService;
            _vacancyService = vacancyService;
            _roleService = roleService;
        }
        
        public IActionResult Register()
        {
            ViewData["roleList"] = _roleService.GetRoleList();
            return View(new UserCreateViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserCreateViewModel user)
        {
            _userService.CreateUser(user);
            return RedirectToAction(nameof(Login));
        }
        
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            var isAuthentication = _userService.GetUser(login);
           
            if (isAuthentication == null)
            {
                ViewData["message"] = "The account is not exist";
                return View(login);
            }
            else
            {            
                var claims = new List<Claim>
                {
                    new Claim("User", isAuthentication.Id.ToString()),
                    new Claim("Role", isAuthentication.Role.Name),
                };
                
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

            return RedirectToAction("Index", "Candidate");
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            
            return RedirectToAction("Login");
        }

        public IActionResult CheckUserByRegister(string userName)
        {
             return new JsonResult(_userService.CheckUserByUserName(userName));
        }
    }
}