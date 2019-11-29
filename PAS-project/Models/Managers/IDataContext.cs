using System.Collections.Generic;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Managers
{
    public interface IDataContext
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Movie> Movies { get; }
        IEnumerable<Seance> Seances { get; }
        IEnumerable<CinemaHall> CinemaHalls { get; }
        IEnumerable<CinemaEvent> CinemaEvents { get; }
    }
}