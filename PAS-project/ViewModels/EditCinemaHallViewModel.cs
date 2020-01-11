using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class EditCinemaHallViewModel
    {
        public CinemaHall CinemaHall { get; set; }
        public int CinemaHallId { get; set; }
        public string SeatId { get; set; }
    }
}