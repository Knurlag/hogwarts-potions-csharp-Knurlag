using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services.Interfaces;

public interface IPotionService
{
    Task<List<Potion>> GetAllPotions();
    Task<Potion> BrewPotion(Student student, List<Ingredient> ingredients);
    Task<List<Potion>> GetAllPotionsOfStudent(long studentId, BrewingStatus brewingStatus);
    Task<Potion> AddIngredientToPotion(long id, Ingredient ingredient);
    Task<List<Recipe>> GetHelp(long id);
    Task<Potion> BrewPotionSlowly(long id);
    Task<Potion> GetPotionById(long? id);
    void Update(Potion potion);
    void RemovePotion(Potion potion);
    Task<List<Recipe>> GetAllRecipes();
}