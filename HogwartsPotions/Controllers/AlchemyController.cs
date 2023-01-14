using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [Authorize]
    public class AlchemyController : Controller
    {
        [Route("Alchemy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
