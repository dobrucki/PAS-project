using System.Collections.Generic;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;
using System.Linq;

namespace PAS_project.Models.Managers
{
    public class SeanceManager
    {
        private readonly IDataRepository<Seance> _seanceRepository;

        public SeanceManager(IDataRepository<Seance> seanceRepository)
        {
            var movie1 = RandomDataFactory.CreateRandomMovie();
            var movie2 = RandomDataFactory.CreateRandomMovie();
            
            seanceRepository.Add(RandomDataFactory.CreateRandomSeance(movie1));
            seanceRepository.Add(RandomDataFactory.CreateRandomSeance(movie1));
            seanceRepository.Add(RandomDataFactory.CreateRandomSeance(movie1));
            seanceRepository.Add(RandomDataFactory.CreateRandomSeance(movie2));
            seanceRepository.Add(RandomDataFactory.CreateRandomSeance(movie2));

            _seanceRepository = seanceRepository;
        }

        public void AddSeance(Seance seance)
        {
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