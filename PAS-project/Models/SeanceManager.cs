using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class SeanceManager
    {
        private readonly IDataRepository<Seance> _repository;

        public SeanceManager(IDataRepository<Seance> repository)
        {
            _repository = repository;
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