using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAS_project.Models.Entities
{
    public class CinemaHall : Entity
    {
        public class Seat
        {
            public char Row { get; set; }
            public int Column { get; set; }
            
            public string PlainComment { get; set; }
        }

        public class WideSeat : Seat
        {
            public string ExtendedComment { get; set; }
        }
        
        public string Name { get; set; }
        public IEnumerable<Seat> Seats { get; set; }

        public Seat GetSeatByString(string rowColumn)
        {
            var col = rowColumn.Substring(1);
            return GetSeatByRowColumn(rowColumn[0], int.Parse(col));
//            return Seats
//                .Where(s => s.Row.Equals(rowColumn[0]))
//                .FirstOrDefault(s => s.Column == int.Parse(col));
        }

        public Seat GetSeatByRowColumn(char row, int column)
        {
            return Seats
                .Where(s => s.Row.Equals(row))
                .FirstOrDefault(s => s.Column.Equals(column));
        }

    }
}