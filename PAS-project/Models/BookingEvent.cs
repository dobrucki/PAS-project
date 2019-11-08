using System;

namespace PAS_project.Models
{
    public class BookingEvent : ICinemaEvent
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public Client BookingClient { get; set; }
        public Seat BookedSeat { get; set; }
        public Seance BookedSeance { get; set; }

        public BookingEvent(Client bookingClient, Seat bookedSeat, Seance bookedSeance)
        {
            BookingClient = bookingClient;
            BookedSeat = bookedSeat;
            BookedSeance = bookedSeance;
        }
    }
}