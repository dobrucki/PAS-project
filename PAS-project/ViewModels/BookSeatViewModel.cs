using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.ViewModels
{
    public class BookSeatViewModel
    {
        public IDictionary<CinemaHall.Seat, bool> Seats { get; set; }
        public Seance Seance { get; set; }
        public int UserId { get; set; }
        public string SeatId { get; set; }
        public int SeanceId { get; set; }
    }
}