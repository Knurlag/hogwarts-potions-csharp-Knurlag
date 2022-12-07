using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BackendTests.Controllers
{
    [TestFixture]
    public class PotionControllerTests
    {
        private Initialize context;

        private PotionController subPotionController;

        [SetUp]
        public void SetUp()
        {
            this.context = new Initialize();
            this.subPotionController = new PotionController(this.context.HogwartsContext);
        }



        [Test]
        public async Task Details_Test()
        {
            // Arrange
            long? id = 5;
            var potion =  context.HogwartsContext.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;
            // Act
            var details = await subPotionController.Details(id) as ViewResult;
            var result = details.Model;
            // Assert
            Assert.That(result, Is.EqualTo(potion));
        }

        [Test]
        public void Create_Test()
        {
            // Arrange
            var result = subPotionController.Create();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Create_Test_1()
        {
            // Arrange
            IngredientListView ingredientList = null;

            // Act
            var result = await subPotionController.Create(
                ingredientList);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Edit_Test()
        {
            // Arrange
            long? id = null;

            // Act
            var result = await subPotionController.Edit(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Edit_Test_1()
        {
            // Arrange
            long id = 0;
            Potion potion = null;

            // Act
            var result = await subPotionController.Edit(
                id,
                potion);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task Delete_Test()
        {
            // Arrange
            long? id = null;

            // Act
            var result = await subPotionController.Delete(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteConfirmed_Test()
        {
            // Arrange
            long id = 0;

            // Act
            var result = await subPotionController.DeleteConfirmed(
                id);

            // Assert
            Assert.Fail();
        }
    }
}
