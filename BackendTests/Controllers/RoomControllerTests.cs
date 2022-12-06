namespace BackendTests.Controllers
{
    [TestFixture]
    public class RoomControllerTests
    {
        private HogwartsContext subHogwartsContext;

        [SetUp]
        public void SetUp()
        {
            this.subHogwartsContext = Substitute.For<HogwartsContext>();
        }

        private RoomController CreateRoomController()
        {
            return new RoomController(
                this.subHogwartsContext);
        }

        [Test]
        public async Task GetAllRooms_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var roomController = this.CreateRoomController();

            // Act
            var result = await roomController.GetAllRooms();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task AddRoom_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var roomController = this.CreateRoomController();
            Room room = null;

            // Act
            await roomController.AddRoom(
                room);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoomById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var roomController = this.CreateRoomController();
            long id = 0;

            // Act
            var result = await roomController.GetRoomById(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void UpdateRoomById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var roomController = this.CreateRoomController();
            long id = 0;
            Room updatedRoom = null;

            // Act
            roomController.UpdateRoomById(
                id,
                updatedRoom);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteRoomById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var roomController = this.CreateRoomController();
            long id = 0;

            // Act
            await roomController.DeleteRoomById(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoomsForRatOwners_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var roomController = this.CreateRoomController();

            // Act
            var result = await roomController.GetRoomsForRatOwners();

            // Assert
            Assert.Fail();
        }
    }
}
