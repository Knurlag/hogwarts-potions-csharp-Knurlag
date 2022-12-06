namespace BackendTests.Controllers
{
    [TestFixture]
    public class AlchemyControllerTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        private AlchemyController CreateAlchemyController()
        {
            return new AlchemyController();
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var alchemyController = this.CreateAlchemyController();

            // Act
            var result = alchemyController.Index();

            // Assert
            Assert.Fail();
        }
    }
}
