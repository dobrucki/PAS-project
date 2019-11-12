using System;

namespace PAS_project.Models
{
    public class CancelBookingEvent : ICinemaEvent
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public Client BookingClient { get; set; }
        public Seat BookedSeat { get; set; }
        public Seance BookedSeance { get; set; }

        public CancelBookingEvent(Client client, Seat seat, Seance seance)
        {
            BookingClient = client;
            BookedSeat = seat;
            BookedSeance = seance;
        }
    }
}