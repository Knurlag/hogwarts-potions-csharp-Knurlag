namespace BackendTests.Controllers
{
    [TestFixture]
    public class PotionControllerTests
    {
        private HogwartsContext subHogwartsContext;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = Substitute.For<HogwartsContext>();
        }

        private PotionController CreatePotionController()
        {
            return new PotionController(
                this.subHogwartsContext);
        }

        [Test]
        public async Task Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var potionController = this.CreatePotionController();

            // Act
            var result = await potionController.Index();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Details_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var potionController = this.CreatePotionController();
            long? id = null;

            // Act
            var result = await potionController.Details(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var potionController = this.CreatePotionController();

            // Act
            var result = potionController.Create();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Create_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var potionController = this.CreatePotionController();
            IngredientListView ingredientList = null;

            // Act
            var result = await potionController.Create(
                ingredientList);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Edit_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var potionController = this.CreatePotionController();
            long? id = null;

            // Act
            var result = await potionController.Edit(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Edit_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var potionController = this.CreatePotionController();
            long id = 0;
            Potion potion = null;

            // Act
            var result = await potionController.Edit(
                id,
                potion);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var potionController = this.CreatePotionController();
            long? id = null;

            // Act
            var result = await potionController.Delete(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteConfirmed_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var potionController = this.CreatePotionController();
            long id = 0;

            // Act
            var result = await potionController.DeleteConfirmed(
                id);

            // Assert
            Assert.Fail();
        }
    }
}
