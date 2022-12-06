namespace BackendTests.Models
{
    [TestFixture]
    public class HogwartsContextTests
    {
        private DbContextOptions<HogwartsContext> subDbContextOptions;

        [SetUp]
        public void SetUp()
        {
            this.subDbContextOptions = Substitute.For<DbContextOptions<HogwartsContext>>();
        }

        private HogwartsContext CreateHogwartsContext()
        {
            return new HogwartsContext(
                this.subDbContextOptions);
        }

        [Test]
        public async Task AddRoom_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            Room room = null;

            // Act
            await hogwartsContext.AddRoom(
                room);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoom_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            long roomId = 0;

            // Act
            var result = await hogwartsContext.GetRoom(
                roomId);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetAllRooms_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();

            // Act
            var result = await hogwartsContext.GetAllRooms();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task UpdateRoom_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            Room room = null;

            // Act
            await hogwartsContext.UpdateRoom(
                room);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteRoom_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            long id = 0;

            // Act
            await hogwartsContext.DeleteRoom(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoomsForRatOwners_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();

            // Act
            var result = await hogwartsContext.GetRoomsForRatOwners();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetAllPotions_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();

            // Act
            var result = await hogwartsContext.GetAllPotions();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task BrewPotion_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            Student student = null;
            List ingredients = null;

            // Act
            var result = await hogwartsContext.BrewPotion(
                student,
                ingredients);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetAllPotionsOfStudent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
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
        public async Task BrewPotionSlowly_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            long id = 0;

            // Act
            var result = await hogwartsContext.BrewPotionSlowly(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task AddIngredientToPotion_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
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
        public async Task GetHelp_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            long id = 0;

            // Act
            var result = await hogwartsContext.GetHelp(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ValidateLogin_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            Student user = null;

            // Act
            var result = hogwartsContext.ValidateLogin(
                user);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Register_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            Student user = null;

            // Act
            var result = hogwartsContext.Register(
                user);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetStudent_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            string username = null;

            // Act
            var result = hogwartsContext.GetStudent(
                username);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetIngredientlistByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hogwartsContext = this.CreateHogwartsContext();
            List potionIngredients = null;

            // Act
            var result = hogwartsContext.GetIngredientlistByName(
                potionIngredients);

            // Assert
            Assert.Fail();
        }
    }
}
