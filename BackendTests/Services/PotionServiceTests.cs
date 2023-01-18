using HogwartsPotions.Data;
using HogwartsPotions.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BackendTests.Services
{
    [TestFixture]
    public class PotionServiceTests
    {
        private HogwartsContext subHogwartsContext;
        private StudentService studentService;
        private IngredientService ingredientService;
        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = new Initialize().HogwartsContext;
            this.studentService = new StudentService(subHogwartsContext);
            this.ingredientService = new IngredientService(subHogwartsContext);
        }

        private PotionService CreateService()
        {
            return new PotionService(
                this.subHogwartsContext);
        }

        [Test]
        public async Task GetAllPotions_Test()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = await service.GetAllPotions();

            // Assert
            Assert.That(subHogwartsContext.Potions.ToList(), Is.EqualTo(result));
        }

        [Test]
        public async Task BrewPotion_Is_Discovery_Test()
        {
            var service = this.CreateService();
            // Arrange
            var potions = service.GetAllPotions().Result;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Alcohol", "Anjelica", "Belladonna", "Armotentia", "Asphodel" };

            // Act
            var result = await service.BrewPotion(studentService.GetStudent("Carson Alexander"), ingredientService.GetIngredientlistByName(ingredientList.Ingredients));

            // Assert
            Assert.That(result.BrewingStatus, Is.EqualTo(BrewingStatus.Discovery));
        }

        [Test]
        public async Task BrewPotion_Is_Replica_Test()
        {
            var service = this.CreateService();
            // Arrange
            var potions = service.GetAllPotions().Result;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Alcohol", "Anjelica", "Belladonna", "Armotentia", "Asphodel" };

            // Act
            await service.BrewPotion(studentService.GetStudent("Carson Alexander"), ingredientService.GetIngredientlistByName(ingredientList.Ingredients));
            var result = await service.BrewPotion(studentService.GetStudent("Carson Alexander"), ingredientService.GetIngredientlistByName(ingredientList.Ingredients));
            // Assert
            Assert.That(result.BrewingStatus, Is.EqualTo(BrewingStatus.Replica));
        }

        [Test] 
        public async Task GetAllPotionsOfStudent_Test()
        {
            var service = this.CreateService();
            long potionId = 2;
            // Arrange
            var student = studentService.GetStudent("Meredith Alonso");
            BrewingStatus status = BrewingStatus.Discovery;
            var expected = new List<Potion>();
            expected.Add(subHogwartsContext.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == potionId).Result);
            // Act
            var result = await service.GetAllPotionsOfStudent(
                student.Id, status);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public async Task AddIngredientToPotion_Replica_Test()
        {
            var service = this.CreateService();
            // Arrange
            long id = 3;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Abraxan hair", "Acromantula venom", "Adder's Fork", "African Red Pepper" };
            var potion = await service.BrewPotion(studentService.GetStudent("Carson Alexander"),
                ingredientService.GetIngredientlistByName(ingredientList.Ingredients));
            // Act
            var result = await service.AddIngredientToPotion(id, ingredientService.GetIngredientByName("African Sea Salt"));

            // Assert
            Assert.That(result.BrewingStatus, Is.EqualTo(BrewingStatus.Replica));
        }

        [Test]
        public async Task AddIngredientToPotion_Discovery_Test()
        {
            var service = this.CreateService();
            // Arrange
            long id = 1;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Alcohol", "Anjelica", "Belladonna", "Armotentia" };
            var potion = await service.BrewPotion(studentService.GetStudent("Carson Alexander"),
                ingredientService.GetIngredientlistByName(ingredientList.Ingredients));
            // Act
            var result = await service.AddIngredientToPotion(3, ingredientService.GetIngredientByName("Asphodel"));

            // Assert
            Assert.That(result.BrewingStatus, Is.EqualTo(BrewingStatus.Discovery));
        }

        [Test]
        public async Task GetHelp_Test()
        {
            var service = this.CreateService();
            // Arrange
            var potion = await service.BrewPotionSlowly(studentService.GetStudent("Arturo Olivetto").Id);
            await service.AddIngredientToPotion(potion.ID, ingredientService.GetIngredientByName("Alcohol"));
            var expected = subHogwartsContext.Recipes
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == potion.ID).Result;
            // Act
            var result = await service.GetHelp(
                3);

            // Assert
            Assert.That(result[1], Is.EqualTo(expected));
        }


        [Test]
        public async Task GetHelp_5Ingredient_Test()
        {
            var service = this.CreateService();
            // Arrange
            long id = 1;
            var expected = subHogwartsContext.Recipes
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == 1).Result;
            // Act
            var result = await service.GetHelp(
                1);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(expected));
        }



        [Test]
        public async Task GetPotionById_Test()
        {
            // Arrange
            var service = this.CreateService();
            var expected = subHogwartsContext.Potions.ToList()[0];

            // Act
            var result = service.GetPotionById(
                1).Result;

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }



        [Test]
        public void RemovePotion_Test()
        {
            // Arrange
            var service = this.CreateService();
            Potion potion = service.GetPotionById(1).Result;
            var expected = subHogwartsContext.Potions.ToList();
            // Act
            service.RemovePotion(
                potion);

            // Assert
            Assert.That(subHogwartsContext.Potions.ToList(), Is.Not.EqualTo(expected));
        }

        [Test]
        public async Task GetAllRecipes_Test()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = await service.GetAllRecipes();

            // Assert
            Assert.That(result, Is.EqualTo(subHogwartsContext.Recipes.ToList()));
        }
        [TearDown]
        public void TearDown()
        {
            subHogwartsContext.Database.EnsureDeleted();
        }
    }
}
