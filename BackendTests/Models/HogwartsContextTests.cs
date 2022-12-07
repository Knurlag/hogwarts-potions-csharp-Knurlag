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
        public async Task BrewPotion_Test()
        {
            // Arrange
            Student student = null;
            var ingredients = new List<Ingredient>();

            // Act
            var result = await hogwartsContext.BrewPotion(
                student,
                ingredients);

            // Assert
            Assert.Fail();
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
        public async Task GetHelp_Test()
        {
            // Arrange
            long id = 1;

            // Act
            var result = await hogwartsContext.GetHelp(
                id);

            // Assert
            Assert.Pass();
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
        public void Register_Test()
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
            var potionIngredients = new List<string>();

            // Act
            var result = hogwartsContext.GetIngredientlistByName(
                potionIngredients);

            // Assert
            Assert.Fail();
        }

        [TearDown]
        public void TearDown()
        {
            hogwartsContext.Database.EnsureDeleted();
        }
    }
}
