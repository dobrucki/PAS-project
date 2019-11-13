using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class HallRepository : IDataRepository<Hall>
    {
        private readonly List<Hall> _halls = new List<Hall>();
        
        public Hall Add(Hall hall)

        {
            var id = _halls.Count == 0 ? 1 : _halls.Last().Id+1;
            hall.Id = id;
            _halls.Add(hall);
            return hall;
        }

        public Hall Get(int id)
        {
            var result = _halls.FirstOrDefault(hall => hall.Id == id);
            return result;
        }

        public IEnumerable<Hall> GetAll()
        {
            return _halls;
        }

        public Hall Update(Hall updatedHall)
        {
            var actualHall = _halls.FirstOrDefault(m => m.Id == updatedHall.Id);
            if (actualHall is null) throw new Exception("Given id does not match any Hall!");
            var actualSeatList = actualHall.GetAllSeats().ToList();
            var updatedSeatList = updatedHall.GetAllSeats().ToList();

            for (var i = 0; i < actualSeatList.Count; i++)
            {
                actualSeatList[i].Number = updatedSeatList[i].Number;
                actualSeatList[i].Row = updatedSeatList[i].Row;
                actualSeatList[i].ScreeningRoom = updatedSeatList[i].ScreeningRoom;
                
            }

            return updatedHall;
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