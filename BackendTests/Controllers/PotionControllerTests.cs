namespace BackendTests.Controllers
{
    [TestFixture]
    public class PotionControllerTests
    {
        private HogwartsContext subHogwartsContext;

        private PotionController subPotionController;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = Substitute.For<HogwartsContext>(Substitute.For<DbContextOptions<HogwartsContext>>());
            this.subPotionController = new PotionController(this.subHogwartsContext);
        }



        [Test]
        public async Task Details_Test()
        {
            // Arrange
            long? id = null;

            // Act
            var result = await subPotionController.Details(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Create_Test()
        {
            // Arrange
            var result = subPotionController.Create();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Create_Test_1()
        {
            // Arrange
            IngredientListView ingredientList = null;

            // Act
            var result = await subPotionController.Create(
                ingredientList);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Edit_Test()
        {
            // Arrange
            long? id = null;

            // Act
            var result = await subPotionController.Edit(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Edit_Test_1()
        {
            // Arrange
            long id = 0;
            Potion potion = null;

            // Act
            var result = await subPotionController.Edit(
                id,
                potion);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Delete_Test()
        {
            // Arrange
            long? id = null;

            // Act
            var result = await subPotionController.Delete(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteConfirmed_Test()
        {
            // Arrange
            long id = 0;

            // Act
            var result = await subPotionController.DeleteConfirmed(
                id);

            // Assert
            Assert.Fail();
        }
    }
}
