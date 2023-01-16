using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        [Route("Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
