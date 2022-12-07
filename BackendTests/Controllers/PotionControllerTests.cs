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
            long? id = 1;
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
        public async Task Create_Test_()
        {
            // Arrange
            var potions = context.HogwartsContext.GetAllPotions().Result;
            IngredientListView ingredientList = new IngredientListView();
            ingredientList.Ingredients = new List<string>{ "Alcohol", "Anjelica", "Belladonna", "Armotentia", "Asphodel" };

            // Act
            await subPotionController.Create(ingredientList);

            // Assert
            Assert.That(context.HogwartsContext.GetAllPotions().Result, Is.Not.EqualTo(potions));
        }


        [Test]
        public async Task Edit_Test()
        {
            // Arrange
            long id = 1;
            
            var potion = context.HogwartsContext.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;

            // Act
            potion.Name = "test";
             await subPotionController.Edit(id, potion);
            var resultpotion = context.HogwartsContext.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;
            // Assert
            Assert.That(resultpotion.Name, Is.EqualTo("test"));
        }



        [Test]
        public async Task Delete_Test()
        {
            // Arrange
            long id = 1;

            // Act
             await subPotionController.DeleteConfirmed(id);
            var result = context.HogwartsContext.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.ID == id).Result;
            // Assert
            Assert.That(result, Is.EqualTo(null));
        }

        [TearDown]
        public void TearDown()
        {
            context.HogwartsContext.Database.EnsureDeleted();
        }
    }
}
