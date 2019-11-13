using System;

namespace PAS_project.Models
{
    public interface ICinemaEvent : IModel
    {
        DateTime EventTime { get; set; }
        User BookingUser { get; set; }
        Seat BookedSeat { get; set; }
        Seance BookedSeance { get; set; }
    }
}