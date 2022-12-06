using HogwartsPotions.data;
using NSubstitute;
using NUnit.Framework;
using System;

namespace BackendTests.Data
{
    [TestFixture]
    public class DbInitializerTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        private DbInitializer CreateDbInitializer()
        {
            return new DbInitializer();
        }

        [Test]
        public void Initialize_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var dbInitializer = this.CreateDbInitializer();
            HogwartsContext context = null;

            // Act
            dbInitializer.Initialize(
                context);

            // Assert
            Assert.Fail();
        }
    }
}
