using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using HogwartsPotions.Data;
using HogwartsPotions.Helpers;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
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

        public IActionResult Register(string username, string password, HouseType houseType, PetType petType)
        {
            Student user = new Student() { Name = username, Password = password, HouseType = houseType, PetType = petType};
            if (_studentService.Register(user))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "username", username);
                return RedirectToAction("Index", "Student");
            }
            var message = "User already exists!";
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
