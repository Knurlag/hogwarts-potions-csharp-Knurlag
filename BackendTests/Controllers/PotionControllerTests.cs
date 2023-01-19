using HogwartsPotions.Data;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BackendTests.Controllers
{
    [TestFixture]
    public class PotionControllerTests
    {
        private HogwartsContext context;

        private PotionController subPotionController;

        [SetUp]
        public async Task SetUp()
        {
            var init = new Initialize();
            this.context = init.HogwartsContext;
            await init.InitializeDb(context);
            this.subPotionController = new PotionController(new PotionService(context), new IngredientService(context), new StudentService(context));
        }

        [Test]
        public async Task Details_Test()
        {
            // Arrange
            long? id = 2;
            var potion =  context.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;
            // Act
            var details = await subPotionController.Details(id) as ViewResult;
            var result = details.Model;
            // Assert
            Assert.That(result, Is.EqualTo(potion));
        }


        [Test]
        public async Task Details_nullId_Test()
        {

            // Arrange
            long? id = null;
            // Act
            var result = await subPotionController.Details(id) as NotFoundResult;
            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(new NotFoundResult().StatusCode));
        }
        [Test]
        public async Task Details_WrongId_Test()
        {
            // Arrange
            long? id = 100;
            // Act
            var result = await subPotionController.Details(id) as NotFoundResult;
            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(new NotFoundResult().StatusCode));
        }

        [Test]
        public async Task Edit_Test()
        {
            // Arrange
            long id = 2;
            
            var potion = context.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;

            // Act
            potion.Name = "test";
             await subPotionController.Edit(id, potion);
            var resultpotion = context.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;
            // Assert
            Assert.That(resultpotion.Name, Is.EqualTo("test"));
        }

        [Test]
        public async Task Edit_WrongId_Test()
        {
            // Arrange
            long id = 1;

            var potion = context.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;

            // Act
            potion.Name = "test";
            var result = await subPotionController.Edit(3, potion) as NotFoundResult;

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(new NotFoundResult().StatusCode));
        }


        [Test]
        public async Task Delete_Test()
        {
            // Arrange
            long id = 1;

            // Act
             await subPotionController.DeleteConfirmed(id);
            var result = context.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;
            // Assert
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task DeleteConfirmed_NullDb_Test()
        {
            // Arrange
            context.Potions = null;
            long id = 1;
            var expected = new ProblemDetails();
            expected.Detail = "Entity set 'HogwartsContext.Potions'  is null.";
            // Act
            var result = await subPotionController.DeleteConfirmed(id) as ObjectResult;
            ProblemDetails detail = (ProblemDetails)result.Value;
            // Assert
            Assert.That(detail.Detail, Is.EqualTo(expected.Detail));
        }

        [TearDown]
        public async Task TearDown()
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
}
