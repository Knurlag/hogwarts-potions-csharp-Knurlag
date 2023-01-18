using HogwartsPotions.Data;
using HogwartsPotions.Services;
using NSubstitute;
using NUnit.Framework;
using System;

namespace BackendTests.Services
{
    [TestFixture]
    public class StudentServiceTests
    {
        private HogwartsContext subHogwartsContext;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = new Initialize().HogwartsContext;
        }

        private StudentService CreateService()
        {
            return new StudentService(
                this.subHogwartsContext);
        }

        [Test]
        public void GetStudent_Test()
        {
            // Arrange
            var service = this.CreateService();
            string username = "Carson Alexander";

            // Act
            var result = service.GetStudent(
                username);

            // Assert
            Assert.That(result.UserName, Is.EqualTo("Carson Alexander"));
        }
        [TearDown]
        public void TearDown()
        {
            subHogwartsContext.Database.EnsureDeleted();
        }
    }
}
