using NUnit.Framework;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;
using PAS_project.Models.Repositories;

namespace UnitTests
{
    [TestFixture]
    public class CinemaEventManagerTest
    {
        private IDataRepository<CinemaEvent> _repository;
        private CinemaEventManager _manager;

        private void InitializeFields()
        {
            _repository = new MockCinemaEventRepository();
            _manager = new CinemaEventManager(_repository);
        }

        [Test]
        public void CreateABooking_ValidArguments_NotNullCinemaEventReturned()
        {
            // Arrange
            InitializeFields();
            var user = new User();
            var seance = new Seance();
            var seat = new CinemaHall.Seat();
            
            // Act
            var booking = _manager.CreateABooking(user, seance, seat);

            // Assert
            Assert.IsNotNull(booking.Id);
            Assert.IsNotNull(booking.Seance);
            Assert.IsNotNull(booking.Seat);
            Assert.IsNotNull(booking.User);
            Assert.IsNotNull(booking.OccurenceTime);
        }
        
        
    }
}