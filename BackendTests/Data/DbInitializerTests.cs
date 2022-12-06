namespace BackendTests.Data
{
    [TestFixture]
    public class DbInitializerTests
    {


        [SetUp]
        public void SetUp()
        {

        }



        [Test]
        public void Initialize_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            HogwartsContext context = null;

            // Act
            DbInitializer.Initialize(
                context);

            // Assert
            Assert.Fail();
        }
    }
}
