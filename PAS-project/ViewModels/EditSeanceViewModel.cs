using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class EditSeanceViewModel
    {
        public int Id { get; set; }
        public Seance Seance { get; set; }
        public int MovieId { get; set; }
        public int CinemaHallId { get; set; }   
        public string DateTime { get; set; }
    }
}