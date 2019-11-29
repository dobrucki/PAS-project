using System;

namespace PAS_project.Models.Entities
{
    public class Seance : Entity
    {
        public Movie Movie { get; set; }
        public CinemaHall CinemaHall { get; set; }
        public DateTime StartingTime { get; set; }
    }
}