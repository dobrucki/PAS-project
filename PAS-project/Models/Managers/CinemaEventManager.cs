using System;
using System.Collections.Generic;
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
            if (correspondingEvents.Any())
            {
                return null;
            }
            if ((seance.StartingTime - DateTime.UtcNow).Minutes <= user.UserType.MinutesForBooking)
            {
                return null;
            }
            var cinemaEvent = new CinemaEvent
            {
                // ReSharper disable once PossibleNullReferenceException
                Id = events.LastOrDefault() is null ? 1 : events.LastOrDefault().Id + 1,
                User = user,
                Seat = seat,
                Seance = seance
            };
            _cinemaEventRepository.Add(cinemaEvent);
            return cinemaEvent;
        }

        public void CancelABooking(CinemaEvent cinemaEvent)
        {
            if ((cinemaEvent.Seance.StartingTime - DateTime.UtcNow)
                .Minutes <= cinemaEvent.User.UserType.MinutesForCancelling)
            {
                _cinemaEventRepository.Delete(cinemaEvent);
            }
        }

        public IEnumerable<CinemaEvent> SearchAllBookings()
        {
            return _cinemaEventRepository.GetAll();
        }

        public IEnumerable<CinemaEvent> SearchByUser(User user)
        {
            bool Filter(CinemaEvent e) => e.User == user;
            return _cinemaEventRepository.GetAll(Filter);
        }

        public IEnumerable<CinemaEvent> SearchBySeance(Seance seance)
        {
            bool Filter(CinemaEvent e) => e.Seance == seance;
            return _cinemaEventRepository.GetAll(Filter);
        }
        
        
        
    }
}