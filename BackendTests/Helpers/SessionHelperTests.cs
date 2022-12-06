using Newtonsoft.Json;
using System.Text;

namespace BackendTests.Helpers
{
    [TestFixture]
    public class SessionHelperTests
    {
        
        private ISession session = new MockHttpSession();
        const string key = "username";

        [SetUp]
        public void SetUp()
        {
        }


        [Test]
        public void GetObjectFromJson_Test()
        {
            SessionHelper.SetObjectAsJson(
                session,
                key,
                "AA");
            // Act
            var result = SessionHelper.GetObjectFromJson<string>(
                session,
                key);
            // Assert
            Assert.That(result, Is.EqualTo("AA")) ;
        }

        [Test]
        public void SetObjectAsJson_Test()
        {

            object value = "AA";

            // Act
            SessionHelper.SetObjectAsJson(
                session,
                key,
            value);

            var result = SessionHelper.GetObjectFromJson<string>(
                session,
                key);
            // Assert
            Assert.That(result, Is.EqualTo("AA"));
        }
    }
}
