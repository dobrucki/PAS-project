using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class EditSeatViewModel
    {
        public CinemaHall CinemaHall { get; set; }
        public CinemaHall.Seat Seat { get; set; }
        public int CinemaHallId { get; set; }
        public string SeatId { get; set; }
        public char SeatRow { get; set; }
        public int SeatColumn { get; set; }
        public string PlainComment { get; set; }
        public string ExtendedComment { get; set; }
    }
}