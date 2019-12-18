using System.ComponentModel.DataAnnotations;

namespace PAS_project.ViewModels
{
    public class AddSeanceViewModel
    {
        [Range(10000, 20000)]
        public int MovieId { get; set; }
        [Range(50000, 60000)]
        public int CinemaHallId { get; set; }
        public string DateTime { get; set; }
    }
}