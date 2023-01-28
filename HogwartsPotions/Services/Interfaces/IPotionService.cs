using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services.Interfaces;

public interface IPotionService
{
    Task<List<Potion>> GetAllPotions();
    bool ArePotionsNull();
    Task<Potion> BrewPotion(Student student, List<Ingredient> ingredients);
    Task<List<Potion>> GetAllPotionsOfStudent(string studentId, BrewingStatus brewingStatus);
    Task<Potion> AddIngredientToPotion(long id, Ingredient ingredient);
    Task<List<Recipe>> GetHelp(long id);
    Task<Potion> BrewPotionSlowly(string id);
    Task<Potion> GetPotionById(long? id);
    Task<bool> Update(Potion potion);
    void RemovePotion(Potion potion);
    Task<List<Recipe>> GetAllRecipes();
}