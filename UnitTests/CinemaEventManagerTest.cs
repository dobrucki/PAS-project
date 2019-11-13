using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PAS_project.Models;

namespace UnitTests
{
    [TestFixture]
    public class CinemaEventManagerTest
    {
        private Client _client;
        private List<Seat> _seats;
        private Hall _hall;
        private Seat _seat;
        private Movie _movie;
        private Seance _seance;
        private CinemaEventRepository _repository;
        private CinemaEventManager _manager;
        
        
        private void ArrangeObjects()
        {
            _client = 
                new Client(
                    "Mateusz", 
                    "Wasilewski", 
                    true, "wasil_98@o2.pl", 
                    "530060545",
                    "92-525"
                );
            
            _seats = new List<Seat>();
            
            _hall = new Hall(_seats);
            _seat = 
                new Seat('A', 12, _hall);
            _seats.Add(_seat);
            _movie = new Movie("xd", "xd");
            _seance =
                new Seance(
                    120,
                    DateTime.UtcNow, 
                    _hall,
                    _movie
                );
            _repository = new CinemaEventRepository();
            _manager = new CinemaEventManager(_repository);
        }
        
        [Test]
        public void MakeABooking_CorrectParams_InsertBookingEventIntoRepository()
        {
            // Arrange
            ArrangeObjects();
            // Act
            var bookingEvent = _manager.MakeABooking(_client, _seance, _seat);
            // Assert
            Assert.AreSame(bookingEvent, _manager.ActiveBookingsForSpecificClient(_client).FirstOrDefault());
        }
        
        [Test]
        public void CancelABooking_CorrectParams_InsertCancelBookingEventIntoRepository()
        {
            // Arrange
            ArrangeObjects();
            var bookingEvent = _manager.MakeABooking(_client, _seance, _seat);
            // Act
            var cancelBookingEvent = _manager.CancelBooking(bookingEvent);
            // Assert
            Assert.AreSame(cancelBookingEvent, _repository.GetAll().LastOrDefault());
        }

        [Test]
        public void ActiveBookingsForSpecificClient_CorrectParams_ReturnsActiveBookings()
        {
            // Arrange
            ArrangeObjects();
            var @event = _manager.MakeABooking(_client, _seance, _seat);
            // Act
            var events = _manager.ActiveBookingsForSpecificClient(_client);
            // Assert
            Assert.AreSame(@event, events.LastOrDefault());
        }
        
        [Test]
        public void ActiveBookingsForSpecificClient_CorrectParams_ReturnsNotCanceledBookings()
        {
            // Arrange
            ArrangeObjects();
            var @event = _manager.MakeABooking(_client, _seance, _seat);
            var cancelEvent = _manager.CancelBooking(@event);
            var newEvent = _manager.MakeABooking(_client, _seance, _seat);
            // Act
            var events = _manager.ActiveBookingsForSpecificClient(_client).ToList();
            // Assert
            Assert.AreEqual(1, events.Count);
            Assert.AreSame(newEvent, events.LastOrDefault());
        }

        [Test]
        public void CanceledBookingEventsForSpecificClient_CorrectParams_ReturnsCanceledBookings()
        {
            // Arrange
            ArrangeObjects();
            var @event = _manager.MakeABooking(_client, _seance, _seat);
            var cancelEvent = _manager.CancelBooking(@event);
            // Act
            var bookings = _manager.CanceledBookingsForSpecificClient(_client).ToList();
            // Assert
            Assert.AreEqual(@event, bookings.LastOrDefault());
        }
    }
}