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

        public CinemaEventManager(IDataRepository<CinemaEvent> cinemaEventRepository, IDataContext dataContext)
        {
            _cinemaEventRepository = cinemaEventRepository;
            if (!(dataContext is null))
            {
                foreach (var c in dataContext.CinemaEvents)
                {
                    _cinemaEventRepository.Add(c);
                }
            }
        }

        public CinemaEvent CreateABooking(User user, Seance seance, CinemaHall.Seat seat)
        {
            if (user.Active is false) return null;
            var events = _cinemaEventRepository.GetAll().ToList();
            var correspondingEvents = events
                .Where(e => e.Seance == seance)
                .Where(e => e.Seat == seat)
                .ToList();
            if (correspondingEvents.Any())
            {
                return null;
            }
            var minutes = seance.StartingTime.Subtract(DateTime.UtcNow).TotalMinutes;
            if (minutes <= user.UserType.MinutesForBooking)
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
            if (cinemaEvent.User.Active is false)
            {
                throw new ArgumentException($"User with id {cinemaEvent.User.Id} is inactive.");
            }
            var minutes = cinemaEvent.Seance.StartingTime.Subtract(DateTime.UtcNow).TotalMinutes;
            if (minutes >= cinemaEvent.User.UserType.MinutesForCancelling)
            {
                _cinemaEventRepository.Delete(cinemaEvent);
            }
            else
            {
                throw new ArgumentException($"It is too late to cancel event with id {cinemaEvent.Id}.");
            }
        }
        public CinemaEvent GetEventById(int id)
        {
            return _cinemaEventRepository.GetById(id);
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

        public IDictionary<CinemaHall.Seat, bool> GetSeatsWithTags(Seance seance)
        {
            var seats = new Dictionary<CinemaHall.Seat, bool>();
            var allEvents = SearchBySeance(seance).ToList();
            foreach (var seat in seance.CinemaHall.Seats.ToList())
            {
                if (allEvents.Any(e => e.Seat == seat))
                {
                    seats.Add(seat, true);
                }
                else
                {
                    seats.Add(seat, false);
                }
            }
            return seats;
        }


    }
}