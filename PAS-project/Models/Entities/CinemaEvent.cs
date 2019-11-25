using System;

namespace PAS_project.Models.Entities
{
    public class CinemaEvent : Entity
    {
        
        public int Id { get; set; }
        public User User { get; set; }
        public CinemaHall.Seat Seat { get; set; }
        public Seance Seance { get; set; }
        public DateTime OccurenceTime { get; }

        public CinemaEvent()
        {
            OccurenceTime = DateTime.UtcNow;
        }
    }
}