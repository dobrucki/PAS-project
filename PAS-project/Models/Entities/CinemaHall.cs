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

        public class WideSeat : Seat
        {
            public string Comment { get; set; }
        }
        
        public string Name { get; set; }
        public IEnumerable<Seat> Seats { get; set; }

        public Seat GetSeatByString(string rowColumn)
        {
            var col = rowColumn.Substring(1);
            return Seats
                .Where(s => s.Row.Equals(rowColumn[0]))
                .FirstOrDefault(s => s.Column == int.Parse(col));
        }

    }
}