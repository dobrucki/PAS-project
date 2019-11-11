using System.Collections.Generic;

namespace PAS_project.Models
{
    public class Hall : IModel
    {
        public int Id { get; set; }
        private readonly IEnumerable<Seat> _seats;
        
        public Hall(IEnumerable<Seat> seats)
        {
            _seats = seats;
        }

        public IEnumerable<Seat> GetAllSeats()
        {
            return _seats;
        }
        
        
    }
}   