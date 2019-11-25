using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;

namespace PAS_project.Models.Repositories
{
    internal class MockCinemaEventRepository : IDataRepository<CinemaEvent>
    {
        private readonly List<CinemaEvent> _cinemaEvents = new List<CinemaEvent>();
        
        public CinemaEvent Add(CinemaEvent item)
        {
            _cinemaEvents.Add(item);
            return item;
        }

        public CinemaEvent Get(int id)
        {
            return _cinemaEvents.LastOrDefault(e => e.Id == id);
        }

        public IEnumerable<CinemaEvent> GetAll()
        {
            return _cinemaEvents;
        }

        public CinemaEvent Update(CinemaEvent item)
        {
            var query = _cinemaEvents.LastOrDefault(e => e.Id == item.Id);
            if (query is null) return null;
            query.Seance = item.Seance;
            query.Seat = item.Seat;
            return query;
        }

        public CinemaEvent Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}