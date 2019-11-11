using System;
using System.Collections.Generic;
using System.Linq;

namespace PAS_project.Models
{
    public class SeanceRepository : IDataRepository<Seance>
    {
        private readonly List<Seance> _seances = new List<Seance>();
        
        public Seance Add(Seance seance)
        {
            var id = _seances.Count == 0 ? 1 : _seances.Last().Id+1;
            seance.Id = id;
            _seances.Add(seance);
            return seance;
        }

        public Seance Get(int id)
        {
            var result = _seances.First(seance => seance.Id == id);
            if (result is null) throw new Exception("Given id does not match any seance!");
            return result;
        }

        public IEnumerable<Seance> GetAll()
        {
            return _seances;
        }

        public Seance Update(Seance updatedSeance)
        {
            if (updatedSeance is null) throw new Exception("Given seance is null!");
            var actualSeance = _seances.FirstOrDefault(m => m.Id == updatedSeance.Id);
            if (actualSeance is null) throw new Exception("Given id does not match any seance!");
            actualSeance.Movie = updatedSeance.Movie;
            actualSeance.DurationTime = updatedSeance.DurationTime;
            actualSeance.ScreeningRoom = updatedSeance.ScreeningRoom;
            actualSeance.SeanceDateTime = updatedSeance.SeanceDateTime;
            return actualSeance;
        }

        public Seance Delete(int id)
        {
            var foundSeance = _seances.FirstOrDefault(c => c.Id == id);
            if(foundSeance is null) throw new Exception("Given id does not match any seance!");
            _seances.Remove(foundSeance);
            return foundSeance;
        }
    }
}