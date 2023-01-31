using System.Threading.Tasks;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [Authorize(Roles = "Student")]
    public class RecipesController : Controller
    {
        private readonly IPotionService _service;

        public RecipesController(IPotionService service)
        {
            _service = service;
        }
        [Route("Recipes")]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllRecipes());
        }
    }
}