using System;

namespace PAS_project.Models
{
    public class CancelBookingEvent : ICinemaEvent
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public User BookingUser { get; set; }
        public Seat BookedSeat { get; set; }
        public Seance BookedSeance { get; set; }

        public CancelBookingEvent(User user, Seat seat, Seance seance)
        {
            BookingUser = user;
            BookedSeat = seat;
            BookedSeance = seance;
        }
    }
}