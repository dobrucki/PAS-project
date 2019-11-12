using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class HallManager
    {
        private readonly IDataRepository<Hall> _repository;

        public HallManager(IDataRepository<Hall> repository)
        {
            _repository = repository;
        }
        
        public Hall AddHall(Hall hall)
        {
            if (hall is null) throw new Exception("Given hall is null");
            var newHallSeats = new HashSet<Seat>(hall.GetAllSeats());
            
            
            if (_repository.GetAll().Select(h => h.GetAllSeats().Any(x => newHallSeats.Contains(x))).Any(result => result == true))
            {
                return null;
            }
            _repository.Add(hall);
            return hall;
        }
        
        public Hall GetHall(int id)
        {
            var result = _repository.Get(id);
            if (result is null) throw new Exception("Given id does not match any Hall!");
            return result;
        }

        public IEnumerable<Hall> GetAllHalls()
        {
            if(!_repository.GetAll().Any()) throw new Exception("There is not any Hall!");
            return _repository.GetAll();
        }

        public Hall UpdateHall(Hall updatedHall)
        {
            if (updatedHall is null) throw new Exception("Given Hall is null!");

            return _repository.Update(updatedHall);
            
        }
        public Hall DeleteHall(int id)
        {
            return _repository.Delete(id);
        }
        
    }
}