using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Http;
using HogwartsPotions.Data;
using HogwartsPotions.Services;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HogwartsPotions.Controllers
{
    [Authorize]
    public class PotionController : Controller
    {
        private readonly IPotionService _service;
        private readonly IIngredientService _ingredientService;
        private readonly IStudentService _studentService;

        public PotionController(IPotionService service, IIngredientService ingredientService, IStudentService studentService)
        {
            _service = service;
            _ingredientService = ingredientService;
            _studentService = studentService;
        }

        // GET: Potion
        [Route("Potion")]
        public async Task<IActionResult> Index()
        {
            
            return View(await _service.GetAllPotions());
        }

        // GET: Potion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _service.GetAllPotions() == null)
            {
                return NotFound();
            }

            var potion = await _service.GetPotionById(id);

            if (potion == null)
            {
                return NotFound();
            }

            ViewBag.Ingredients = potion.Ingredients; 
            return View(potion);
        }

        // GET: Potion/Create
        public IActionResult Create()
        {
            ViewBag.Ingredients = _ingredientService.GetAllIngredients();
            //ViewBag.Ingredients = new MultiSelectList(_context.Ingredients.ToList(), "Name", "Name");
            ViewBag.Username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            return View();
        }

    // POST: Potion/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ingredients")] IngredientListView ingredientList)
        {
            List<Ingredient> ingredients = _ingredientService.GetIngredientlistByName(ingredientList.Ingredients);
            var username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            var student = _studentService.GetStudent(username);
            if (ModelState.IsValid)
            {
                await _service.BrewPotion(student, ingredients);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredientList);
        }

        // GET: Potion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || await _service.GetAllPotions() == null)
            {
                return NotFound();
            }

            var potion = _service.GetPotionById(id).Result;
            if (potion == null)
            {
                return NotFound();
            }
            return  View(potion);
        }

        // POST: Potion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Name,BrewingStatus")] Potion potion)
        {
            if (id != potion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _service.Update(potion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PotionExists(potion.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(potion);
        }

        // GET: Potion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _service.GetAllPotions().Result == null)
            {
                return NotFound();
            }

            var potion = await _service.GetPotionById(id);
            if (potion == null)
            {
                return NotFound();
            }

            return View(potion);
        }

        // POST: Potion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_service.GetAllPotions().Result == null)
            {
                return Problem("Entity set 'HogwartsContext.Potions'  is null.");
            }
            var potion = await _service.GetPotionById(id);
            if (potion != null)
            {
                _service.RemovePotion(potion);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PotionExists(long id)
        {
            return _service.GetAllPotions().Result.Any(e => e.ID == id);
        }
    }
}

