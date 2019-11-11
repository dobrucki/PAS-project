using System;

namespace PAS_project.Models
{
    public class Seance : IModel
    {
        public int Id { get; set; }
        public int DurationTime { get; set; }
        public DateTime SeanceDateTime { get; set; }
        public Hall ScreeningRoom { get; set; }
        public Movie Movie { get; set; }

        public Seance(int durationTime, DateTime seanceDateTime, Hall screeningRoom, Movie movie)
        {
            DurationTime = durationTime;
            SeanceDateTime = seanceDateTime;
            ScreeningRoom = screeningRoom;
            Movie = movie;
        }
    }
}