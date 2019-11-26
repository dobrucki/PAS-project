using System.Collections.Generic;

namespace PAS_project.Models.Entities
{
    public class CinemaHall : Entity
    {
        public class Seat
        {
            public int Row { get; set; }
            public int Column { get; set; }
        }
        
        public string Name { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public Seance Seance { get; set; }
        
    }
}