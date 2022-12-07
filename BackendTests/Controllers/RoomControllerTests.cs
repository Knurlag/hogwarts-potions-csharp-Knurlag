namespace BackendTests.Controllers
{
    [TestFixture]
    public class RoomControllerTests
    {

        private RoomController subRoomController;
        private Initialize context;

        [SetUp]
        public void SetUp()
        {
            this.context = new Initialize();
            this.subRoomController = new RoomController(context.HogwartsContext);
        }


        [Test]
        public async Task GetAllRooms_Test()
        {
            // Arrange
            var rooms = context.HogwartsContext.GetAllRooms().Result;
            // Act
            var result = await subRoomController.GetAllRooms();

            // Assert
            Assert.That(result, Is.EqualTo(rooms));
        }

        [Test]
        public async Task AddRoom_Test()
        {
            // Arrange

            Room room = null;

            // Act
            await subRoomController.AddRoom(
                room);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoomById_Test()
        {
            // Arrange

            long id = 0;

            // Act
            var result = await subRoomController.GetRoomById(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void UpdateRoomById_Test()
        {
            // Arrange

            long id = 0;
            Room updatedRoom = null;

            // Act
            subRoomController.UpdateRoomById(
                id,
                updatedRoom);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeleteRoomById_Test()
        {
            // Arrange

            long id = 0;

            // Act
            await subRoomController.DeleteRoomById(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetRoomsForRatOwners_Test()
        {
            // Arrange


            // Act
            var result = await subRoomController.GetRoomsForRatOwners();

            // Assert
            Assert.Fail();
        }

        [TearDown]
        public void TearDown()
        {
            context.HogwartsContext.Database.EnsureDeleted();
        }
    }
}
