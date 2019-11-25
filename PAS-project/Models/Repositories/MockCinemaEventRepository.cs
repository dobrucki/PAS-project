using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using PAS_project.Models.Entities;

[assembly: InternalsVisibleTo("UnitTests")]
namespace PAS_project.Models.Repositories
{
    internal class MockCinemaEventRepository : IDataRepository<CinemaEvent>
    {
        private readonly List<CinemaEvent> _cinemaEvents = new List<CinemaEvent>();
        
        public void Add(CinemaEvent cinemaEvent)
        {
            _cinemaEvents.Add(cinemaEvent);
        }

        public CinemaEvent GetById(int id)
        {
            return _cinemaEvents.LastOrDefault(e => e.Id == id);
        }

        public IEnumerable<CinemaEvent> GetAll(Func<CinemaEvent, bool> predicate)
        {
            return _cinemaEvents.Where(predicate).AsEnumerable();
        }

        public IEnumerable<CinemaEvent> GetAll()
        {
            return _cinemaEvents.AsEnumerable();
        }

        public void Update(CinemaEvent cinemaEvent)
        {
            var previousEvent = GetById(cinemaEvent.Id);
            previousEvent.Seance = cinemaEvent.Seance;
            previousEvent.Seat = cinemaEvent.Seat;
            previousEvent.User = cinemaEvent.User;
        }

        public void Delete(CinemaEvent cinemaEvent)
        {
            _cinemaEvents.Remove(cinemaEvent);
        }
    }
}