using System.Collections.Generic;

namespace PAS_project.Models
{
    public class Hall : IModel
    {
        public int Id { get; set; }
        private List<Seat> _seatsList;
        
        public Hall()
        {
            _seatsList = new List<Seat>();
        }
        
    }
}   