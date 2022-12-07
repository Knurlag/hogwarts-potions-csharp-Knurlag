namespace BackendTests.Controllers
{
    [TestFixture]
    public class StudentControllerTests
    {
        private HogwartsContext subHogwartsContext;
        private StudentController subStudentController;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = Substitute.For<HogwartsContext>(Substitute.For<DbContextOptions<HogwartsContext>>());
            this.subStudentController = new StudentController(subHogwartsContext);
        }


        [Test]
        public void ValidateLogin_Test()
        {
            // Arrange


            // Act
            var result = subStudentController.ValidateLogin();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Register_Test()
        {
            // Arrange


            // Act
            var result = subStudentController.Register();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Logout_Test()
        {
            // Arrange


            // Act
            var result = subStudentController.Logout();

            // Assert
            Assert.Fail();
        }
    }
}
