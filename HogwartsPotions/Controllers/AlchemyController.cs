using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class AlchemyController : Controller
    {
        [Route("Alchemy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
