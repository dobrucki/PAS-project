using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class HallRepository
    {
        private readonly List<Hall> _halls = new List<Hall>();
        
        public void Add(Hall item)

        {
            var id = _halls.Count == 0 ? 1 : _halls.Last().Id+1;
            item.Id = id;
            _halls.Add(item);
        }

        public Hall Get(int id)
        {
            var result = _halls.First(hall => hall.Id == id);
            if (result is null) throw new Exception("Given id does not match any Hall!");
            return result;
        }

        public IEnumerable<Hall> GetAll()
        {
            return _halls;
        }

        public Hall Update(Hall updatedHall)
        {
            if (updatedHall is null) throw new Exception("Given Hall is null!");
            var actualHall = _halls.FirstOrDefault(m => m.Id == updatedHall.Id);
            if (actualHall is null) throw new Exception("Given id does not match any Hall!");
            var actualSeatList = actualHall.GetAllSeats().ToList();
            var updatedSeatList = updatedHall.GetAllSeats().ToList();
            for (var i = 0; i < updatedSeatList.Count(); i++)
            {
                if (updatedSeatList[i] != actualSeatList[i])
                {
                    actualSeatList[i] = updatedSeatList[i];
                }
            }
            return actualHall;
        }

        public Hall Delete(int id)
        {
            var foundHall = _halls.FirstOrDefault(h => h.Id == id);
            if(foundHall is null) throw new Exception("Given id does not match any Hall!");
            _halls.Remove(foundHall);
            return foundHall;
        }
    }
}