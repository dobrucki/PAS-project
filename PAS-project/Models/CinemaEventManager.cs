using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class CinemaEventManager
    {
        private readonly IDataRepository<ICinemaEvent> _repository;

        public CinemaEventManager(IDataRepository<ICinemaEvent> repository)
        {
            _repository = repository;
        }

        public BookingEvent MakeABooking(Client client, Seance seance, Seat seat)
        {
            if (client is null || seance is null || seat is null)
            {
                throw new Exception("Invalid booking data");
            }

            var events = _repository.GetAll()
                .Where(e => e.BookedSeance == seance)
                .Where(e => e.BookedSeat == seat);
            if (events.Last().GetType() == typeof(BookingEvent))
            {
                throw new Exception("Seat already booked");
            }

            var bookingEvent = new BookingEvent(client, seat, seance);
            _repository.Add(bookingEvent);
            return bookingEvent;
        }

        public CancelBookingEvent CancelBooking(BookingEvent bookingEvent)
        {
            var client = bookingEvent.BookingClient;
            var seance = bookingEvent.BookedSeance;
            var seat = bookingEvent.BookedSeat;

            var events = _repository.GetAll()
                .Where(e => e.BookingClient == client)
                .Where(e => e.BookedSeance == seance)
                .Where(e => e.BookedSeat == seat);
            if (events.Last() != bookingEvent) throw new Exception("Could not cancel booking");
            var cancel = new CancelBookingEvent(client, seat, seance);
            _repository.Add(cancel);
            return cancel;

        }

        private IEnumerable<ICinemaEvent> BookingsForSpecificClient(Client client)
        {
            return _repository.GetAll()
                .Where(b => b.BookingClient == client)
                .Where(b => b.GetType() == typeof(BookingEvent));
        }

        public IEnumerable<ICinemaEvent> ActiveBookingsForSpecificClient(Client client)
        {
            return BookingsForSpecificClient(client).Except(CanceledBookingsForSpecificClient(client));
        }

        public IEnumerable<ICinemaEvent> CanceledBookingsForSpecificClient(Client client)
        {
            var bookings = BookingsForSpecificClient(client).ToList();
            var cancels = _repository.GetAll().Where(b => b.BookingClient == client).Except(bookings).ToList();
            
            var bookingsSeances = new HashSet<Seance>(bookings.Select(b => b.BookedSeance));
            var canceledBookings = cancels.Where(c => bookingsSeances.Contains(c.BookedSeance));

            return canceledBookings;
        }
    }
}