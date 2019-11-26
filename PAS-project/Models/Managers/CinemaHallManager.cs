using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class CinemaHallManager
    {
        private readonly IDataRepository<CinemaHall> _cinemaHallRepository;

        public CinemaHallManager(IDataRepository<CinemaHall> cinemaHallRepository)
        {
            _cinemaHallRepository = cinemaHallRepository;
        }

        public void AddCinemaHall(CinemaHall cinemaHall)
        {
            _cinemaHallRepository.Add(cinemaHall);
        }

        public CinemaHall GetCinemaHallById(int id)
        {
            return _cinemaHallRepository.GetById(id);
        }
        
        public CinemaHall GetCinemaHallBySeance(Seance seance)
        {
            return _cinemaHallRepository.GetAll().FirstOrDefault(entity => entity.Seance == seance);
        }

        public IEnumerable<CinemaHall> GetAllCinemaHalls()
        {
            return _cinemaHallRepository.GetAll();
        }
        
        public void UpdateCinemaHall(CinemaHall cinemaHall)
        {
            _cinemaHallRepository.Update(cinemaHall);
        }

        public void DeleteCinemaHall(CinemaHall cinemaHall)
        {
            _cinemaHallRepository.Delete(cinemaHall);
        }
    }
}