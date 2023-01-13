using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class PotionService : IPotionService
{
    private readonly HogwartsContext _context;

    public PotionService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task<List<Potion>> GetAllPotions()
    {
        return await _context.Potions.Include(potion => potion.Recipe).Include(potion => potion.BrewerStudent).ToListAsync();
    }

    public async Task<Potion> BrewPotion(Student student, List<Ingredient> ingredients)
    {
        var status = BrewingStatus.Discovery;
        foreach (var recipe in _context.Recipes.Include(recipe => recipe.Ingredients))
        {
            var index = -1;
            var ingredientCounter = 0;
            foreach (var ingredient in ingredients)
            {
                index++;
                if (ingredient.Name == recipe.Ingredients[index].Name)
                {
                    ingredientCounter++;
                }
            }

            if (ingredientCounter == 5)
            {
                status = BrewingStatus.Replica;
            }
        }

        var name = $"{student.UserName}'s Discovery #{GetAllPotionsOfStudent(student.Id, BrewingStatus.Discovery).Result.Count + 1}";
        var potionRecipe = new Recipe { Ingredients = ingredients, Name = name, Student = student };
        var potion = new Potion
        {
            Name = name,
            Recipe = potionRecipe,
            BrewerStudent = student,
            BrewingStatus = status
        };
        if (status == BrewingStatus.Discovery)
        {

           _context.Potions.Add(potion);
           _context.Recipes.Add(potionRecipe);
           _context.SaveChanges();
           return await Task.FromResult(potion);
        }
        potion.Name = $"{student.UserName}'s Replica #{GetAllPotionsOfStudent(student.Id, BrewingStatus.Replica).Result.Count + 1}";
        _context.Potions.Add(potion);
        await _context.SaveChangesAsync();
        return await Task.FromResult(potion);
    }

    public async Task<List<Potion>> GetAllPotionsOfStudent(string studentId, BrewingStatus brewingStatus)
    {
        var potions = await _context.Potions.Where(potion => potion.BrewerStudent.Id == studentId).ToListAsync();
        return  potions.Where(potion => potion.BrewingStatus == brewingStatus).ToList();
    }

    public async Task<Potion> AddIngredientToPotion(long id, Ingredient ingredient)
    {

        var potionToUpdate = await _context.Potions.FirstOrDefaultAsync(p => p.ID == id);
        bool isDiscovery = true;
        var recipes = _context.Recipes.ToList();
        if (potionToUpdate.Recipe.Ingredients.Count >= _context.MaxIngredientsForPotions - 1)
        {
            //potionToUpdate.Ingredients.Add(ingredient);
            potionToUpdate.Recipe.Ingredients.Add(ingredient);
            foreach (var recipe in _context.Recipes.Include(recipe => recipe.Ingredients))
            {
                var index = -1;
                var ingredientCounter = 0;
                foreach (var currentIngredient in potionToUpdate.Recipe.Ingredients)
                {
                    index++;
                    if (currentIngredient.Name == recipe.Ingredients[index].Name)
                    {
                        ingredientCounter++;
                    }
                }

                if (recipe != recipes[^1])
                {
                    if (ingredientCounter == 5)
                    {
                        isDiscovery = false;
                    }
                }
            }

            if (isDiscovery)
            {
                potionToUpdate.BrewingStatus = BrewingStatus.Discovery;
                _context.Update(potionToUpdate);
                await _context.SaveChangesAsync();
            }
            else
            {
                potionToUpdate.BrewingStatus = BrewingStatus.Replica;
                _context.Update(potionToUpdate);
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            //potionToUpdate.Ingredients.Add(ingredient);
            potionToUpdate.Recipe.Ingredients.Add(ingredient);
            _context.Update(potionToUpdate);
            await _context.SaveChangesAsync();
        }


        return potionToUpdate;
    }

    public async Task<List<Recipe>> GetHelp(long id)
    {
        var potion = await _context.Potions.FirstOrDefaultAsync(p => p.ID == id);
        var recipesWithSameIngredients = new List<Recipe>();
        if (potion.Recipe.Ingredients.Count <= _context.MaxIngredientsForPotions)
        {

            foreach (var recipe in _context.Recipes.Include(r => r.Ingredients))
            {
                var numOfSameIngredients = 0;
                foreach (var ingredient in potion.Recipe.Ingredients)
                {
                    if (recipe.Ingredients.Contains(ingredient))
                    {
                        numOfSameIngredients++;
                        if (numOfSameIngredients <= potion.Recipe.Ingredients.Count)
                        {
                            if (!recipesWithSameIngredients.Contains(recipe))
                            {
                                recipesWithSameIngredients.Add(recipe);
                            }

                        }
                    }
                }

            }
            return recipesWithSameIngredients;
        }

        return null;
    }

    public Task<Potion> BrewPotionSlowly(string id)
    {
        var student = _context.Students.First(p => p.Id == id);
        var potion = new Potion { BrewerStudent = student, BrewingStatus = BrewingStatus.Brew, Recipe = new Recipe() };
        _context.Potions.Add(potion);
        _context.SaveChanges();
        return Task.FromResult(potion);
    }

    public async Task<Potion> GetPotionById(long? id)
    {
        return await _context.Potions.Include(potion => potion.Ingredients).FirstOrDefaultAsync(potion => potion.ID == id);
    }

    public void Update(Potion potion)
    {
        _context.Update(potion);
        _context.SaveChanges();
    }

    public void RemovePotion(Potion potion)
    {
        _context.Potions.Remove(potion);
        _context.SaveChanges();
    }

    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await _context.Recipes.Include(p => p.Ingredients).Include(m => m.Student).ToListAsync();
    }
    
}
