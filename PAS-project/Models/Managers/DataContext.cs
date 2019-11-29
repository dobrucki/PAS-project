using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Managers
{
    public class DataContext : IDataContext
    {
        public IEnumerable<User> Users { get; internal set; }
        public IEnumerable<Movie> Movies { get; internal set; }
        public IEnumerable<Seance> Seances { get; internal set; }
        public IEnumerable<CinemaHall> CinemaHalls { get; internal set; }
        public IEnumerable<CinemaEvent> CinemaEvents { get; internal set; }
    }
}