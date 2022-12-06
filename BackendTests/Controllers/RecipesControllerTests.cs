namespace BackendTests.Controllers
{
    [TestFixture]
    public class RecipesControllerTests
    {
        private HogwartsContext subHogwartsContext;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = Substitute.For<HogwartsContext>();
        }

        private RecipesController CreateRecipesController()
        {
            return new RecipesController(
                this.subHogwartsContext);
        }

        [Test]
        public async Task Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var recipesController = this.CreateRecipesController();

            // Act
            var result = await recipesController.Index();

            // Assert
            Assert.Fail();
        }
    }
}
