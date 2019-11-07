using System.Collections.Generic;

namespace PAS_project.Models
{
    public class Hall
    {
        public int Number { get; set; }
        private List<Seat> _seatsList;
        
        public Hall()
        {
            _seatsList = new List<Seat>();
        }
        
    }
}   