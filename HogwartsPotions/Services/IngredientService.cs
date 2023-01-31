using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Services.Interfaces;

namespace HogwartsPotions.Services;

public class IngredientService : IIngredientService
{
    private readonly HogwartsContext _context;

    public IngredientService(HogwartsContext context)
    {
        _context = context;
    }
    public List<Ingredient> GetIngredientlistByName(List<string> potionIngredients)
    {
        List<Ingredient> result = new List<Ingredient>();
        foreach (var ingredientstr in potionIngredients)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(i => i.Name == ingredientstr);
            if (ingredient == null)
            {
                //TODO add Error logging
            }
            result.Add(ingredient);
        }

        return result;
    }

    public Ingredient GetIngredientByName(string ingredient)
    {
        Ingredient result = _context.Ingredients.FirstOrDefault(i => i.Name == ingredient);
        if (result == null)
        {
            //TODO add Error logging
        }
        return result;
    }

    public List<Ingredient> GetAllIngredients()
    {
        return _context.Ingredients.ToList(); 
    }
}