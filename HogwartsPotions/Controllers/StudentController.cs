using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;

        public StudentController(UserManager<Student> userManager, SignInManager<Student> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }
        public  IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.HouseTypes = new HouseType[] {HouseType.Gryffindor, HouseType.Hufflepuff, HouseType.Ravenclaw, HouseType.Slytherin};
            ViewBag.PetTypes = new PetType[] {PetType.Cat, PetType.Owl, PetType.Rat, PetType.None};
            return View();
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateLogin(LoginForm loginForm)
        {
            await HttpContext.SignOutAsync("Identity.Application");
            if (loginForm == null)
            {
                HttpContext.Session.SetString("message", "Please enter the correct credentials!");
                return RedirectToAction("Index");
            }
            var result = await _signInManager.PasswordSignInAsync(loginForm.Username, loginForm.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
                {
                    //SessionHelper.SetObjectAsJson(HttpContext.Session, "username", loginForm.Username);
                    return RedirectToAction("Index", "Home");
                }


            var message = "Please enter the correct credentials!";
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterForm registerForm)
        {
            if (registerForm == null)
            {
                HttpContext.Session.SetString("message", "Please enter a username and a password");
                return RedirectToAction("Index");
            }
            Student user = new Student() { UserName = registerForm.Username, HouseType = registerForm.HouseType, PetType = registerForm.PetType};
            var result = await _userManager.CreateAsync(user, registerForm.Password);
            Student student = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(student, "Student");
            if (result.Succeeded)
            {
                //SessionHelper.SetObjectAsJson(HttpContext.Session, "username", registerForm.Username);
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

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("username");
            await HttpContext.SignOutAsync("Identity.Application");
            return RedirectToAction("Index", "Student");
        }

    }
}
