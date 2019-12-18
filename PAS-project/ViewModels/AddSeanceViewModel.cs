using System.ComponentModel.DataAnnotations;

namespace PAS_project.ViewModels
{
    public class AddSeanceViewModel
    {
        [Range(10000, 20000)]
        [Display(Name = "Movie ID")]
        public int MovieId { get; set; }
        [Range(50000, 60000)]
        [Display(Name = "CinemaHall ID")]
        public int CinemaHallId { get; set; }
        [Display(Name = "Starting time")]
        public string DateTime { get; set; }
    }
}