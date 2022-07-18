using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            foreach (var room in Rooms.Include(room=>room.Residents).ToList())
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

        public async Task UpdateRoom(Room room)
        {
            throw new NotImplementedException();
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
            var goodRooms = new List<Room>();
            foreach (var room in Rooms.Include(r=>r.Residents).ToList())
            {
                if (room.Residents.Count == 0)
                {
                    goodRooms.Add(room);
                }
                else
                {
                    foreach (var student in room.Residents)
                    {
                        if (student.PetType is PetType.None or PetType.Rat)
                        {
                            goodRooms.Add(room);
                        }
                    }
                }
            }
            return Task.FromResult(goodRooms);
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await Potions.Include(potion=>potion.Ingredients).Include(potion=>potion.BrewerStudent).ToListAsync();
        }

        public Task<Potion> BrewPotion(long id,List<Ingredient> ingredients)
        {
            var student = Students.First(p => p.ID == id);
            var status = BrewingStatus.Discovery;
            foreach (var recipe in Recipes.Include(recipe=>recipe.Ingredients))
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


            var name = $"{student.Name}'s Discovery #{GetAllPotionsOfStudent(student.ID).Result.Count + 1}";
            var potion = new Potion { BrewerStudent = student, BrewingStatus = status, Ingredients = ingredients, Name = name };
            if (status == BrewingStatus.Discovery)
            {
                var recipe = new Recipe { Ingredients = ingredients, Name = name };
                Potions.Add(potion);
                Recipes.Add(recipe);
                SaveChanges();
            }
            potion.Name = $"{student.Name}'s Replica #{GetAllPotionsOfStudent(student.ID).Result.Count + 1}";
            Potions.Add(potion);
            SaveChanges();
            return Task.FromResult(potion);



        }

        public Task<List<Potion>> GetAllPotionsOfStudent(long id)
        {
            List<Potion> potionsOfStudent = new List<Potion>();
            foreach (var potion in Potions.Include(potion=>potion.BrewerStudent).Include(potion=>potion.Ingredients))
            {
                if (id == potion.BrewerStudent.ID)
                {
                    potionsOfStudent.Add(potion);
                }
            }
            return Task.FromResult(potionsOfStudent);
        }

        public Task<Potion> BrewPotionSlowly(long id)
        {
            var student = Students.First(p => p.ID == id);
            var potion = new Potion {BrewerStudent = student, BrewingStatus = BrewingStatus.Brew};
            Potions.Add(potion);
            SaveChanges();
            return Task.FromResult(potion);
        }

        public async Task<Potion> AddIngredientToPotion(long id, Ingredient ingredient)
        {

            var potionToUpdate = await Potions.FirstOrDefaultAsync(p => p.ID == id);
            if (potionToUpdate.Ingredients.Count == 5)
            {
                foreach (var recipe in Recipes)
                {
                    if (potionToUpdate.Ingredients.Equals(recipe.Ingredients))
                    {
                        potionToUpdate.BrewingStatus = BrewingStatus.Replica;
                        Update(potionToUpdate);
                        await SaveChangesAsync();
                    }
                    else
                    {
                        potionToUpdate.BrewingStatus = BrewingStatus.Discovery;
                        Update(potionToUpdate);
                        await SaveChangesAsync();
                    }
                }

            }
            else
            {
                potionToUpdate.Ingredients.Add(ingredient);
                Update(potionToUpdate);
                await SaveChangesAsync();
            }
            return potionToUpdate;
        }

        public async Task<List<Recipe>> GetHelp(long id)
        {
            var potion = await Potions.FirstOrDefaultAsync(p => p.ID == id);
            var recipesWithSameIngredients = new List<Recipe>();
            if (potion.Ingredients.Count < 5)
            {
                
                foreach (var recipe in Recipes.Include(r=>r.Ingredients))
                {
                    var numOfSameIngredients = 0;
                    foreach (var ingredient in potion.Ingredients)
                    {
                        if (recipe.Ingredients.Contains(ingredient))
                        {
                            numOfSameIngredients++;
                            if (numOfSameIngredients <= potion.Ingredients.Count)
                            {
                                recipesWithSameIngredients.Add(recipe);
                            }
                        }
                    }

                }
                return recipesWithSameIngredients;
            }

            return recipesWithSameIngredients;
        }
    }
}