using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class UserDetailsViewModel
    {
        public User User { get; set; }

        public IEnumerable<CinemaEvent> CinemaEvents { get; set; }
    }

    
}