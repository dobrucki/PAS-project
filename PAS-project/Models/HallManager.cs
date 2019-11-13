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
            var seats = new List<Seat>();
            var hall = new Hall(seats);
            seats.Add(new Seat('A', 1, hall));
            AddHall(hall);
        }
        
        public Hall AddHall(Hall hall)
        {
            if (hall is null) throw new Exception("Given hall is null");
            
            if (_repository.GetAll().Select(h => h.GetAllSeats().Select(x => x)
                .Intersect(hall.GetAllSeats())
                .Any()).Any(hasMatch => hasMatch))
            {
                throw new Exception("Hall with these seats already exist!");
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