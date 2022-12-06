using HogwartsPotions.Helpers;
using NSubstitute;
using NUnit.Framework;
using System;

namespace BackendTests.Helpers
{
    [TestFixture]
    public class SessionHelperTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        private SessionHelper CreateSessionHelper()
        {
            return new SessionHelper();
        }

        [Test]
        public void SetObjectAsJson_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sessionHelper = this.CreateSessionHelper();
            ISession session = null;
            string key = null;
            object value = null;

            // Act
            sessionHelper.SetObjectAsJson(
                session,
                key,
                value);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetObjectFromJson_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var sessionHelper = this.CreateSessionHelper();
            ISession session = null;
            string key = null;

            // Act
            var result = sessionHelper.GetObjectFromJson(
                session,
                key);

            // Assert
            Assert.Fail();
        }
    }
}
