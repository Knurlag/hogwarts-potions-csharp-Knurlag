namespace BackendTests.Models
{
    [TestFixture]
    public class HogwartsContextTests
    {
        private HogwartsContext hogwartsContext;


        [SetUp]
        public void SetUp()
        {

            this.hogwartsContext = new Initialize().HogwartsContext;
        }


        [Test]
        public async Task AddRoom_Test()
        {
            // Arrange

            Room room = null;

            // Act
            await hogwartsContext.AddRoom(
                room);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoom_Test()
        {
            // Arrange

            long roomId = 0;

            // Act
            var result = await hogwartsContext.GetRoom(
                roomId);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetAllRooms_Test()
        {
            // Act
            var result = await hogwartsContext.GetAllRooms();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task UpdateRoom_Test()
        {
            // Arrange
            Room room = null;

            // Act
            await hogwartsContext.UpdateRoom(
                room);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteRoom_Test()
        {
            // Arrange
            long id = 0;

            // Act
            await hogwartsContext.DeleteRoom(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoomsForRatOwners_Test()
        {
            // Arrange
            // Act
            var result = await hogwartsContext.GetRoomsForRatOwners();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetAllPotions_Test()
        {
            // Arrange
            // Act
            var result = await hogwartsContext.GetAllPotions();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task BrewPotion_Test()
        {
            // Arrange
            Student student = null;
            var ingredients = new List<Ingredient>();

            // Act
            var result = await hogwartsContext.BrewPotion(
                student,
                ingredients);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetAllPotionsOfStudent_Test()
        {
            // Arrange
            long id = 0;
            BrewingStatus status = default(global::HogwartsPotions.Models.Enums.BrewingStatus);

            // Act
            var result = await hogwartsContext.GetAllPotionsOfStudent(
                id,
                status);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task BrewPotionSlowly_Test()
        {
            // Arrange
            long id = 0;

            // Act
            var result = await hogwartsContext.BrewPotionSlowly(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task AddIngredientToPotion_Test()
        {
            // Arrange
            long id = 0;
            Ingredient ingredient = null;

            // Act
            var result = await hogwartsContext.AddIngredientToPotion(
                id,
                ingredient);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetHelp_Test()
        {
            // Arrange
            long id = 0;

            // Act
            var result = await hogwartsContext.GetHelp(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ValidateLogin_Test()
        {
            // Arrange
            Student user = null;

            // Act
            var result = hogwartsContext.ValidateLogin(
                user);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Register_Test()
        {
            // Arrange
            Student user = null;

            // Act
            var result = hogwartsContext.Register(
                user);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetStudent_Test()
        {
            // Arrange
            string username = null;

            // Act
            var result = hogwartsContext.GetStudent(
                username);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetIngredientlistByName_Test()
        {
            // Arrange
            var potionIngredients = new List<string>();

            // Act
            var result = hogwartsContext.GetIngredientlistByName(
                potionIngredients);

            // Assert
            Assert.Fail();
        }

        [TearDown]
        public void TearDown()
        {
            hogwartsContext.Database.EnsureDeleted();
        }
    }
}
