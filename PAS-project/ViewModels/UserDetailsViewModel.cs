using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class UserDetailsViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<CinemaEvent> CinemaEvents { get; set; }
        
    }

    
}