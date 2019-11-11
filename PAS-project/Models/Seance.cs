using System;

namespace PAS_project.Models
{
    public class Seance : IModel
    {
        public int Id { get; set; }
        public int DurationTime { get; set; }
        public DateTime SeanceDateTime { get; set; }
        public Hall ScreeningRoom { get; set; }
        public Movie ProjectedMovie { get; set; }
    }
}