using System.Collections.Generic;
using HogwartsPotions.Helpers;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class UserController : Controller
    {
        private readonly HogwartsContext _context;

        public UserController(HogwartsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidateLogin()
        {
            string username = Request.Form["login-username"];
            string password = Request.Form["login-password"];
            User user = new User() { Username = username, Password = password };
            if (_context.ValidateLogin(user))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "username", username);
                return RedirectToAction("Index", "Potion");
            }

            var message = "Please enter the correct credentials!";
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            string username = Request.Form["register-username"];
            string password = Request.Form["register-password"];
            string email = Request.Form["register-email"];
            User user = new User() { Username = username, Password = password,};
            var message = "User already exists!";
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index", "Product");
        }

    }
}
