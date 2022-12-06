using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
