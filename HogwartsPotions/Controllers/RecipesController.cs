using System.Threading.Tasks;
using HogwartsPotions.Data;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Controllers
{
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