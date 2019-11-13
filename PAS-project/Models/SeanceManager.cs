using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class SeanceManager
    {
        private readonly IDataRepository<Seance> _repository;
        private readonly HallManager _hallManager;
        private readonly MovieManager _movieManager;

        public SeanceManager(IDataRepository<Seance> repository, HallManager hallManager, MovieManager movieManager)
        {
            _repository = repository;
            _hallManager = hallManager;
            _movieManager = movieManager;
            var hall = _hallManager.GetAllHalls().FirstOrDefault();
            var movie1 = _movieManager.GetAllMovies().FirstOrDefault();
            var movie2 = _movieManager.GetAllMovies().LastOrDefault();
            var seance1 = new Seance(120, DateTime.Now, hall, movie1);
            var seance2 = new Seance(110, DateTime.Now, hall, movie2);
            AddSeance(seance1);
            AddSeance(seance2);
        }
        
        public Seance AddSeance(Seance seance)
        {
            if(seance is null) throw new Exception("Given seance is null");
            if (_repository.GetAll().
                Any(s => s.Movie == seance.Movie && 
                         s.SeanceDateTime == seance.SeanceDateTime && 
                         s.ScreeningRoom == seance.ScreeningRoom)) 
                throw new Exception("Given seance already exists!");
            _repository.Add(seance);
            return seance;
        }
        
        public Seance GetSeance(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Seance> GetAllSeances()
        {
            return _repository.GetAll();
        }

        public Seance UpdateSeance(Seance updatedSeance)
        {
            if (updatedSeance is null) throw new Exception("Given Seance is null!");

            return _repository.Update(updatedSeance);
            
        }
        public Seance DeleteSeance(int id)
        {
            return _repository.Delete(id);
        }
        public IEnumerable<Seance> FilterByMovie(Movie movie)
        {
            return _repository.GetAll().Where(sc => sc.Movie == movie);
        }
        
        
        
    }
}