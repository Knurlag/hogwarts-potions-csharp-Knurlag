namespace BackendTests.Controllers
{
    [TestFixture]
    public class StudentControllerTests
    {
        private HogwartsContext subHogwartsContext;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = Substitute.For<HogwartsContext>();
        }

        private StudentController CreateStudentController()
        {
            return new StudentController(
                this.subHogwartsContext);
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var studentController = this.CreateStudentController();

            // Act
            var result = studentController.Index();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ValidateLogin_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var studentController = this.CreateStudentController();

            // Act
            var result = studentController.ValidateLogin();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Register_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var studentController = this.CreateStudentController();

            // Act
            var result = studentController.Register();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Logout_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var studentController = this.CreateStudentController();

            // Act
            var result = studentController.Logout();

            // Assert
            Assert.Fail();
        }
    }
}
