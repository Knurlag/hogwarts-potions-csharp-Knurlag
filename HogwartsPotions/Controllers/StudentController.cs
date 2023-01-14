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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;

        public StudentController(IStudentService studentService, UserManager<Student> userManager, SignInManager<Student> signInManager)
        {
            _studentService = studentService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            ViewBag.HouseTypes = new HouseType[] {HouseType.Gryffindor, HouseType.Hufflepuff, HouseType.Ravenclaw, HouseType.Slytherin};
            ViewBag.PetTypes = new PetType[] {PetType.Cat, PetType.Owl, PetType.Rat, PetType.None};
            return View();
        }

        public async Task<IActionResult> ValidateLogin(LoginForm loginForm)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var result = await _signInManager.PasswordSignInAsync(loginForm.Username, loginForm.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "username", loginForm.Username);
                    return RedirectToAction("Index", "Home");
                }


            var message = "Please enter the correct credentials!";
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Register(RegisterForm registerForm)
        {

            Student user = new Student() { UserName = registerForm.Username, HouseType = registerForm.HouseType, PetType = registerForm.PetType};
            var result = await _userManager.CreateAsync(user, registerForm.Password);
            var student = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(student, "Student");
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
