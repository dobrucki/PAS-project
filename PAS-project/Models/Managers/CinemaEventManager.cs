using System;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class CinemaEventManager
    {
        private IDataRepository<CinemaEvent> _cinemaEventRepository;

        public CinemaEventManager(IDataRepository<CinemaEvent> cinemaEventRepository)
        {
            _cinemaEventRepository = cinemaEventRepository;
        }

        public CinemaEvent CreateABooking(User user, Seance seance, CinemaHall.Seat seat)
        {
            var events = _cinemaEventRepository.GetAll().ToList();
            var correspondingEvents = events
                .Where(e => e.Seance == seance)
                .Where(e => e.Seat == seat)
                .ToList();
            if (correspondingEvents.Any() && correspondingEvents.Last().EventType != EventType.Cancel)
            {
                return null;
            }

            if ((seance.StartingTime - DateTime.UtcNow).Minutes <= user.UserType.MinutesForBooking) return null;
            var cinemaEvent = new CinemaEvent
            {
                Id = events.LastOrDefault() is null ? 1 : events.LastOrDefault().Id + 1,
                EventType = EventType.Booking,
                Seance = seance,
                User = user
            };
            _cinemaEventRepository.Add(cinemaEvent);
            return cinemaEvent;
        }
    }
}