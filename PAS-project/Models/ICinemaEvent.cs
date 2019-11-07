using System;

namespace PAS_project.Models
{
    public interface ICinemaEvent
    {
        int Id { get; set; }
        DateTime EventTime { get; set; }
        Client BookingClient { get; set; }
        Seat BookedSeat { get; set; }
    }
}