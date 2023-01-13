using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HogwartsPotions.Data;
using HogwartsPotions.Helpers;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<Student> _userManager;

        public StudentController(IStudentService studentService, UserManager<Student> userManager)
        {
            _studentService = studentService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.HouseTypes = new HouseType[] {HouseType.Gryffindor, HouseType.Hufflepuff, HouseType.Ravenclaw, HouseType.Slytherin};
            ViewBag.PetTypes = new PetType[] {PetType.Cat, PetType.Owl, PetType.Rat, PetType.None};
            return View();
        }

        public IActionResult ValidateLogin(string username, string password)
        {
            LoginForm loginForm = new LoginForm(username, password);
            if (username != null && password != null)
            {
                if (_studentService.ValidateLogin(loginForm))
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "username", username);
                    return RedirectToAction("Index", "Home");
                }
            }


            var message = "Please enter the correct credentials!";
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Register(RegisterForm registerForm)
        {

            Student user = new Student() { UserName = registerForm.Username, HouseType = registerForm.HouseType, PetType = registerForm.PetType};
            var result = await _userManager.CreateAsync(user, registerForm.Password);
            if (result.Succeeded)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "username", registerForm.Username);
                return RedirectToAction("Index", "Student");
            }
            var message = "";
            foreach (var error in result.Errors)
            {
                message += error.Description;
            }

            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Student");
        }

    }
}
