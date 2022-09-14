using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore.Query;

namespace HogwartsPotions.data
{
    public static class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var rooms = new Room[]
            {
                new Room{Capacity = 5},
                new Room{Capacity = 5}
            };

            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();
            var students = new Student[]
            {
                new Student{Name="Carson Alexander",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = rooms[0]},
                new Student{Name="Meredith Alonso",HouseType = HouseType.Gryffindor, PetType = PetType.Owl, Room = rooms[0]},
                new Student{Name="Arturo Anand",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = rooms[0]},
                new Student{Name="Gytis Barzdukas",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = rooms[0]},
                new Student{Name="Yan Li",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = rooms[0]},
                new Student{Name="Peggy Justice",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = rooms[1]},
                new Student{Name="Laura Alexander",HouseType = HouseType.Slytherin, PetType = PetType.None, Room = rooms[1]},
                new Student{Name="Nino Alexander",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = rooms[1]},
                new Student{Name="Arturo Olivetto",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = rooms[1]},
                new Student{Name="Carson Norman",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = rooms[1]},
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);

            }
            context.SaveChanges();


            var ingredients = new Ingredient[]
            {
                new Ingredient {Name = "Abraxan hair"}, new Ingredient {Name = "Wolfsbane"},
                new Ingredient {Name = "Acromantula venom"}, new Ingredient {Name = "Adder's Fork"},
                new Ingredient {Name = "African Red Pepper"}, new Ingredient {Name = "African Sea Salt"},
                new Ingredient {Name = "Agrippa"}, new Ingredient {Name = "Alcohol"},
                new Ingredient {Name = "Alihotsy"}, new Ingredient {Name = "Angel's Trumpet"},
                new Ingredient {Name = "Anjelica"}, new Ingredient {Name = "Antimony"},
                new Ingredient {Name = "Armadillo bile"}, new Ingredient {Name = "Armotentia"},
                new Ingredient {Name = "Arnica"}, new Ingredient {Name = "Asian Dragon Hair"},
                new Ingredient {Name = "Ashwinder egg"}, new Ingredient {Name = "Asphodel"},
                new Ingredient {Name = "Avocado"}, new Ingredient {Name = "Balm"},
                new Ingredient {Name = "Banana"}, new Ingredient {Name = "Baneberry"},
                new Ingredient {Name = "Bat spleen"}, new Ingredient {Name = "Bat wing"},
                new Ingredient {Name = "Beetle Eye"}, new Ingredient {Name = "Belladonna"},
                new Ingredient {Name = "Betony"}, new Ingredient {Name = "Bezoar"},
                new Ingredient {Name = "Bicorn Horn"}, new Ingredient {Name = "Billywig sting"},
                new Ingredient {Name = "Billywig Sting Slime"}, new Ingredient {Name = "Billywig wings"},
                new Ingredient {Name = "Bitter root"}, new Ingredient {Name = "Blatta Pulvereus"},
                new Ingredient {Name = "Blind-worm's Sting"}, new Ingredient {Name = "Blood"},
                new Ingredient {Name = "Bloodroot"}, new Ingredient {Name = "Unicorn blood"},
                new Ingredient {Name = "Re'em blood"}, new Ingredient {Name = "Blowfly"},
                new Ingredient {Name = "Bone"}, new Ingredient {Name = "Boom Berry"},
                new Ingredient {Name = "Boomslang"}, new Ingredient {Name = "Boomslang Skin"},
                new Ingredient {Name = "Borage"}, new Ingredient {Name = "Bouncing Bulb"},
                new Ingredient {Name = "Bouncing Spider Juice"}, new Ingredient {Name = "Bubotuber pus"},
                new Ingredient {Name = "Bulbadox juice"}, new Ingredient {Name = "Bundimun Secretion"},
                new Ingredient {Name = "Bursting mushroom"}, new Ingredient {Name = "Butterscotch"},
                new Ingredient {Name = "Camphirated Spirit"}, new Ingredient {Name = "Castor oil"},
                new Ingredient {Name = "Cat Hair"}, new Ingredient {Name = "Caterpillar"},
                new Ingredient {Name = "Centaury"}, new Ingredient {Name = "Cheese"},
                new Ingredient {Name = "Chicken Lips"}, new Ingredient {Name = "Chinese Chomping Cabbage"},
                new Ingredient {Name = "Chizpurfle Carapace"}, new Ingredient {Name = "Chizpurfle fang"},
                new Ingredient {Name = "Cinnamon"}, new Ingredient {Name = "Cockroach"},
                new Ingredient {Name = "Corn starch"}, new Ingredient {Name = "Cowbane"},
                new Ingredient {Name = "Crocodile Heart"}, new Ingredient {Name = "Daisy"},
                new Ingredient {Name = "Dandelion root"}, new Ingredient {Name = "Dandruff"}

            };

            foreach (Ingredient ingredient in ingredients)
            {
                context.Ingredients.Add(ingredient);

            }
            context.SaveChanges();
            var recipes = new Recipe[]
            {
                new Recipe{Name = "Potions 1 recipe",Ingredients = new List<Ingredient> { ingredients[0],
                    ingredients[1],
                    ingredients[2],
                    ingredients[3],
                    ingredients[4]}},
                new Recipe{Name = "Potions 2 recipe",Ingredients = new List<Ingredient> {ingredients[5],
                    ingredients[6],
                    ingredients[7],
                    ingredients[8],
                    ingredients[9]}}
            };


            foreach (Recipe recipe in recipes)
            {
                context.Recipes.Add(recipe);

            }
            context.SaveChanges();



            var potions = new Potion[]
            {
                new Potion {BrewerStudent = students[0], BrewingStatus = BrewingStatus.Discovery,Recipe = context.Recipes.ToList()[0] , Name = "Potions 1"},
                new Potion {BrewerStudent = students[1], BrewingStatus = BrewingStatus.Discovery,Recipe = context.Recipes.ToList()[1], Name = "Potions 2"}
            };

            foreach (Potion potion in potions)
            {
                context.Potions.Add(potion);

            }

            context.SaveChanges();
        }
    }
}