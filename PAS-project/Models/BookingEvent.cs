using System;

namespace PAS_project.Models
{
    public class BookingEvent : ICinemaEvent
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public Client BookingClient { get; set; }
        public Seat BookedSeat { get; set; }
    }
}