using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class CinemaEventRepository : IDataRepository<ICinemaEvent>
    {
        private readonly List<ICinemaEvent> _cinemaEvents = new List<ICinemaEvent>();
        
        public ICinemaEvent Add(ICinemaEvent cinemaEvent)
        {
            var id = _cinemaEvents.Count == 0 ? 1 : _cinemaEvents.Last().Id;
            cinemaEvent.Id = id;
            cinemaEvent.EventTime = DateTime.UtcNow;
            _cinemaEvents.Add(cinemaEvent);
            return cinemaEvent;
        }

        public ICinemaEvent Get(int id)
        {
            var foundCinemaEvent = _cinemaEvents.First(client => client.Id == id);
            if (foundCinemaEvent is null) throw new Exception("Given id does not match any cinemaEvent!");
            return foundCinemaEvent;
        }

        public IEnumerable<ICinemaEvent> GetAll()
        {
            return _cinemaEvents;
        }

        public ICinemaEvent Update(ICinemaEvent updatedCinemaEvent)
        {
            if (updatedCinemaEvent is null) throw new Exception("Given cinemaEvent is null!");
            var actualCinemaEvent = _cinemaEvents.FirstOrDefault(c => c.Id == updatedCinemaEvent.Id);
            if (actualCinemaEvent is null) throw new Exception("Given id does not match any client!");
            actualCinemaEvent.BookedSeance = updatedCinemaEvent.BookedSeance;
            actualCinemaEvent.BookedSeat = updatedCinemaEvent.BookedSeat;
            actualCinemaEvent.BookingClient = updatedCinemaEvent.BookingClient;
            actualCinemaEvent.EventTime = updatedCinemaEvent.EventTime;
            return  actualCinemaEvent;
        }

        public ICinemaEvent Delete(int id)
        {
            var foundCinemaEvent = _cinemaEvents.FirstOrDefault(c => c.Id == id);
            if(foundCinemaEvent is null) throw new Exception("Given id does not match any client!");
            _cinemaEvents.Remove(foundCinemaEvent);
            return foundCinemaEvent;
        }
    }
}