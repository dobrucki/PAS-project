using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models.Entities
{
    public class CinemaHall : Entity
    {
        public class Seat
        {
            public char Row { get; set; }
            public int Column { get; set; }
        }
        
        public string Name { get; set; }
        public IEnumerable<Seat> Seats { get; set; }

    }
}