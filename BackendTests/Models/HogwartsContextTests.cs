using HogwartsPotions.Models.Entities;
using NuGet.Protocol;

namespace BackendTests.Models
{
    [TestFixture]
    public class HogwartsContextTests
    {
        private HogwartsContext hogwartsContext;


        [SetUp]
        public void SetUp()
        {

            this.hogwartsContext = new Initialize().HogwartsContext;
        }



        [Test]
        public async Task BrewPotion_Is_Discovery_Test()
        {
            // Arrange
            var potions = hogwartsContext.GetAllPotions().Result;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Alcohol", "Anjelica", "Belladonna", "Armotentia", "Asphodel" };

            // Act
             var result = await hogwartsContext.BrewPotion(hogwartsContext.GetStudent("Carson Alexander"), hogwartsContext.GetIngredientlistByName(ingredientList.Ingredients));

            // Assert
            Assert.That(BrewingStatus.Discovery, Is.EqualTo(result.BrewingStatus));
        }

        [Test]
        public async Task BrewPotion_Is_Replica_Test()
        {
            // Arrange
            var potions = hogwartsContext.GetAllPotions().Result;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Alcohol", "Anjelica", "Belladonna", "Armotentia", "Asphodel" };

            // Act
            await hogwartsContext.BrewPotion(hogwartsContext.GetStudent("Carson Alexander"), hogwartsContext.GetIngredientlistByName(ingredientList.Ingredients));
            var result = await hogwartsContext.BrewPotion(hogwartsContext.GetStudent("Carson Alexander"), hogwartsContext.GetIngredientlistByName(ingredientList.Ingredients));
            // Assert
            Assert.That(BrewingStatus.Replica, Is.EqualTo(result.BrewingStatus));
        }

        [Test]
        public async Task GetAllPotionsOfStudent_Test()
        {
            // Arrange
            long id = 1;
            BrewingStatus status = BrewingStatus.Discovery;
            var expected = new List<Potion>();
            expected.Add(hogwartsContext.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result);
            // Act
            var result = await hogwartsContext.GetAllPotionsOfStudent(
                id, status);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public async Task BrewPotionSlowly_Test()
        {
            // Arrange
            long id = 1;

            // Act
            var result = await hogwartsContext.BrewPotionSlowly(id);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task AddIngredientToPotion_Replica_Test()
        {
            // Arrange
            long id = 3;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Abraxan hair", "Acromantula venom", "Adder's Fork", "African Red Pepper" };
            var potion = await hogwartsContext.BrewPotion(hogwartsContext.GetStudent("Carson Alexander"),
                hogwartsContext.GetIngredientlistByName(ingredientList.Ingredients));
            // Act
            var result = await hogwartsContext.AddIngredientToPotion(id, hogwartsContext.GetIngredientByName("African Sea Salt"));

            // Assert
            Assert.That(result.BrewingStatus, Is.EqualTo(BrewingStatus.Replica));
        }

        [Test]
        public async Task AddIngredientToPotion_Discovery_Test()
        {
            // Arrange
            long id = 1;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string> { "Alcohol", "Anjelica", "Belladonna", "Armotentia" };
            var potion = await hogwartsContext.BrewPotion(hogwartsContext.GetStudent("Carson Alexander"),
                hogwartsContext.GetIngredientlistByName(ingredientList.Ingredients));
            // Act
            var result = await hogwartsContext.AddIngredientToPotion(3, hogwartsContext.GetIngredientByName("Asphodel"));

            // Assert
            Assert.That(result.BrewingStatus, Is.EqualTo(BrewingStatus.Discovery));
        }
        [Test]
        public async Task GetHelp_Test()
        {
            // Arrange
            long id = 1;
            var potion = await hogwartsContext.BrewPotionSlowly(id);
            await hogwartsContext.AddIngredientToPotion(3, hogwartsContext.GetIngredientByName("Alcohol"));
            var expected = hogwartsContext.Recipes
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == 2).Result;
            // Act
            var result = await hogwartsContext.GetHelp(
                3);

            // Assert
            Assert.That(result[0], Is.EqualTo(expected));
        }


        [Test]
        public async Task GetHelp_5Ingredient_Test()
        {
            // Arrange
            long id = 1;
            var expected = hogwartsContext.Recipes
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == 1).Result;
            // Act
            var result = await hogwartsContext.GetHelp(
                1);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(expected));
        }


        [Test]
        public void ValidateLogin_Test()
        {
            // Arrange
            Student user = hogwartsContext.GetStudent("Carson Alexander");

            // Act
            var result = hogwartsContext.ValidateLogin(
                user);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Register_Test_false()
        {
            // Arrange
            Student user = hogwartsContext.GetStudent("Carson Alexander"); ;

            // Act
            var result = hogwartsContext.Register(
                user);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void Register_Test_true()
        {
            // Arrange
            Student user = new Student();
            user.Name = "tester";

            // Act
            var result = hogwartsContext.Register(
                user);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void GetStudent_Test()
        {
            // Arrange
            string username = "Carson Alexander";

            // Act
            var result = hogwartsContext.GetStudent(
                username);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Carson Alexander"));
        }

        [Test]
        public void GetIngredientlistByName_Test()
        {
            // Arrange
            var potionIngredients = new List<string>{"Alcohol"};

            // Act
            var result = hogwartsContext.GetIngredientlistByName(
                potionIngredients);

            // Assert
            Assert.That(result[0].Name, Is.EqualTo("Alcohol"));
        }

        [TearDown]
        public void TearDown()
        {
            hogwartsContext.Database.EnsureDeleted();
        }
    }
}
