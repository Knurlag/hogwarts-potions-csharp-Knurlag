using HogwartsPotions.Models.Entities;
using System.Collections.Generic;

namespace HogwartsPotions.Services.Interfaces;

public interface IIngredientService
{
    List<Ingredient> GetIngredientlistByName(List<string> potionIngredients);
    Ingredient GetIngredientByName(string ingredient);
    List<Ingredient> GetAllIngredients();
}