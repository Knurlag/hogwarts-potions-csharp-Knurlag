namespace BackendTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        private HomeController CreateHomeController()
        {
            return new HomeController();
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var homeController = this.CreateHomeController();

            // Act
            var result = homeController.Index();

            // Assert
            Assert.Fail();
        }
    }
}
