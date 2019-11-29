using System.Collections.Generic;
using System.Linq;
using PAS_project.Models.Entities;
using PAS_project.Models.Repositories;

namespace PAS_project.Models.Managers
{
    public class CinemaHallManager
    {
        private readonly IDataRepository<CinemaHall> _cinemaHallRepository;

        public CinemaHallManager(IDataRepository<CinemaHall> cinemaHallRepository, IDataContext dataContext)
        {
            _cinemaHallRepository = cinemaHallRepository;
            if (!(dataContext is null))
            {
                foreach (var s in dataContext.CinemaHalls)
                {
                    _cinemaHallRepository.Add(s);
                }
            }
        }

        public void AddCinemaHall(CinemaHall cinemaHall)
        {
            _cinemaHallRepository.Add(cinemaHall);
        }

        public CinemaHall GetCinemaHallById(int id)
        {
            return _cinemaHallRepository.GetById(id);
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