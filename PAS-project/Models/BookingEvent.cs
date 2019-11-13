using System;

namespace PAS_project.Models
{
    public class BookingEvent : ICinemaEvent
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public User BookingUser { get; set; }
        public Seat BookedSeat { get; set; }
        public Seance BookedSeance { get; set; }

        public BookingEvent(User bookingUser, Seat bookedSeat, Seance bookedSeance)
        {
            BookingUser = bookingUser;
            BookedSeat = bookedSeat;
            BookedSeance = bookedSeance;
        }
    }
}