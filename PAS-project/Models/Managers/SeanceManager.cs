using System;
using System.Collections.Generic;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;
using System.Linq;

namespace PAS_project.Models.Managers
{
    public class SeanceManager
    {
        public static bool SeanceTimeOverlapFilter(Seance s1, Seance s2) =>
            s1.CinemaHall.Id == s2.CinemaHall.Id
            && s1.StartingTime < s2.StartingTime.AddMinutes(s2.Movie.DurationTime)
            && s2.StartingTime < s1.StartingTime.AddMinutes(s1.Movie.DurationTime);
        
        private readonly IDataRepository<Seance> _seanceRepository;
        private readonly IDataRepository<Movie> _movieRepository;

        public SeanceManager(IDataRepository<Seance> seanceRepository, IDataRepository<Movie> movieRepository, 
            IDataContext dataContext)
        {
            _seanceRepository = seanceRepository;
            _movieRepository = movieRepository;
            if (!(dataContext is null))
            {
                foreach (var s in dataContext.Seances)
                {
                    _seanceRepository.Add(s);
                }
            }
        }

        public void AddSeance(Seance seance)
        {
            if (_seanceRepository.GetAll(s => s.Id.Equals(seance.Id)).Any())
            {
                throw new ArgumentException("Given seance already exists.");
            }

            if (_seanceRepository
                .GetAll(s => s.CinemaHall.Id.Equals(seance.CinemaHall.Id))
                .Any(s => SeanceTimeOverlapFilter(s, seance)))
            {
                throw new ArgumentException("Already exists seance in given time span.");
            }
                
            _seanceRepository.Add(seance);
        }

        public Seance GetSeanceById(int id)
        {
            return _seanceRepository.GetById(id);
        }

        public Seance GetSeanceByMovie(Movie movie)
        {

            bool Filter(Seance seance) => seance.Movie == movie; 
            return _seanceRepository.GetAll(Filter).First();
        }
        
        public IEnumerable<Seance> GetAllSeances()
        {
            return _seanceRepository.GetAll();
        }

        public IEnumerable<Seance> FilterSeancesByTitle(string title)
        {
            bool Filter(Seance seance) => seance.Movie.Title.ToLower().Contains(title.ToLower());
            return _seanceRepository.GetAll(Filter);
        }
        
        public void UpdateSeance(Seance seance)
        {
            _seanceRepository.Update(seance);
        }

        public void DeleteSeance(Seance seance)
        {
            _seanceRepository.Delete(seance);
        }
        
    }
}