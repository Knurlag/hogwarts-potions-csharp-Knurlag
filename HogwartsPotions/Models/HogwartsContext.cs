using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Potion> Potions { get; set; }


        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Recipe>().ToTable("Recipes");
            modelBuilder.Entity<Potion>().ToTable("Potions");

        }

        public async Task AddRoom(Room room)
        {

            Rooms.Add(room);
            await SaveChangesAsync();
        }

        public Task<Room> GetRoom(long roomId)
        {
            foreach (var room in Rooms.Include(room => room.Residents).ToList())
            {
                if (room.ID == roomId)
                {
                    return Task.FromResult(room);
                }
            }
            return Task.FromResult<Room>(null);
        }

        public Task<List<Room>> GetAllRooms()
        {
            return Rooms.Include(room => room.Residents).ToListAsync();
        }


        public async Task DeleteRoom(long id)
        {
            foreach (var room in Rooms.ToList())
            {
                if (room.ID == id)
                {
                    Rooms.Remove(room);
                    await SaveChangesAsync();
                }
            }
        }


        public Task<List<Room>> GetRoomsForRatOwners()
        {
            
            var goodRooms = Rooms.Include(r => r.Residents).ToList();
            var allRooms = Rooms.Include(r => r.Residents).ToList();
            foreach (var room in allRooms)
            {
                var petTypes = new List<PetType>();
                foreach (var student in room.Residents)
                {
                    petTypes.Add(student.PetType);
                }

                if (petTypes.Contains(PetType.Cat) || petTypes.Contains(PetType.Owl))
                {
                    goodRooms.Remove(room);
                }
            }
            return Task.FromResult(goodRooms);
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await Potions.Include(potion => potion.Recipe).Include(potion => potion.BrewerStudent).ToListAsync();
        }

        public Task<Potion> BrewPotion(Student student, List<Ingredient> ingredients)
        {
            var status = BrewingStatus.Discovery;
            foreach (var recipe in Recipes.Include(recipe => recipe.Ingredients))
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


            var name = $"{student.Name}'s Discovery #{GetAllPotionsOfStudent(student.ID, BrewingStatus.Discovery).Result.Count + 1}";
            var potionRecipe = new Recipe { Ingredients = ingredients, Name = name, Student = student};
            var potion = new Potion { BrewerStudent = student, Ingredients = ingredients, BrewingStatus = status, Recipe = potionRecipe, Name = name };
            if (status == BrewingStatus.Discovery)
            {
                
                Potions.Add(potion); 
                Recipes.Add(potionRecipe);
                SaveChanges();
                return Task.FromResult(potion);
            }
            potion.Name = $"{student.Name}'s Replica #{GetAllPotionsOfStudent(student.ID, BrewingStatus.Replica).Result.Count + 1}";
            Potions.Add(potion);
            SaveChanges();
            return Task.FromResult(potion);



        }

        public Task<List<Potion>> GetAllPotionsOfStudent(long id, BrewingStatus status)
        {
            List<Potion> potionsOfStudent = new List<Potion>();
            foreach (var potion in Potions.Include(potion => potion.BrewerStudent).Include(potion => potion.Recipe))
            {
                if (id == potion.BrewerStudent.ID)
                {
                    if (potion.BrewingStatus == status)
                    {
                        potionsOfStudent.Add(potion);
                    }
                }
            }
            return Task.FromResult(potionsOfStudent);
        }

        public Task<Potion> BrewPotionSlowly(long id)
        {
            var student = Students.First(p => p.ID == id);
            var potion = new Potion { BrewerStudent = student, BrewingStatus = BrewingStatus.Brew, Recipe = new Recipe()};
            Potions.Add(potion);
            SaveChanges();
            return Task.FromResult(potion);
        }

        public async Task<Potion> AddIngredientToPotion(long id, Ingredient ingredient)
        {

            var potionToUpdate = await Potions.FirstOrDefaultAsync(p => p.ID == id);
            bool isDiscovery = false;
            var recipes = Recipes.ToList();
            if (potionToUpdate.Recipe.Ingredients.Count >= MaxIngredientsForPotions-1)
            {
                //potionToUpdate.Ingredients.Add(ingredient);
                potionToUpdate.Recipe.Ingredients.Add(ingredient);
                foreach (var recipe in recipes)
                {
                    if (recipe != recipes[recipes.Count - 1])
                    {
                        if (!potionToUpdate.Recipe.Ingredients.Equals(recipe.Ingredients))
                        {
                            isDiscovery = true;
                        }
                        else
                        {
                            isDiscovery = false;
                        }
                    }
                }
                if (isDiscovery)
                {
                    potionToUpdate.BrewingStatus = BrewingStatus.Discovery;
                    Update(potionToUpdate);
                    await SaveChangesAsync();
                }
                else
                {
                    potionToUpdate.BrewingStatus = BrewingStatus.Replica;
                    Update(potionToUpdate);
                    await SaveChangesAsync();
                }
            }
            else
            {
                //potionToUpdate.Ingredients.Add(ingredient);
                potionToUpdate.Recipe.Ingredients.Add(ingredient);
                Update(potionToUpdate);
                await SaveChangesAsync();
            }


            return potionToUpdate;
        }

        public async Task<List<Recipe>> GetHelp(long id)
        {
            var potion = await Potions.FirstOrDefaultAsync(p => p.ID == id);
            var recipesWithSameIngredients = new List<Recipe>();
            if (potion.Recipe.Ingredients.Count <= MaxIngredientsForPotions)
            {

                foreach (var recipe in Recipes.Include(r => r.Ingredients))
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

        public bool ValidateLogin(Student user)
        {
            return Students.Any(u => u.Name == user.Name && u.Password == user.Password);
        }

        private bool CheckRegistrationStatus(Student user)
        {
            var u = Students.FirstOrDefault(u => u.Name == user.Name);
            return u == null;
        }

        public bool Register(Student user)
        {
            if (CheckRegistrationStatus(user))
            {
                Students.Add(user);
                SaveChanges();
                return true;
            }

            return false;
        }

        public Student GetStudent(string username)
        {
            var student = Students.FirstOrDefault(p => p.Name == username);
            if (student == null)
            {
                //TODO add Error logging
            }
            return student;
        }

        public List<Ingredient> GetIngredientlistByName(List<string> potionIngredients)
        {
            List<Ingredient> result = new List<Ingredient>();
            foreach (var ingredientstr in potionIngredients)
            {
                var ingredient = Ingredients.FirstOrDefault(i => i.Name == ingredientstr);
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
            Ingredient result = Ingredients.FirstOrDefault(i => i.Name == ingredient);
            if (result == null)
            {
                //TODO add Error logging
            }
            return result;
        }
    }
}