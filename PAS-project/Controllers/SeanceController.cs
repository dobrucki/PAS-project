using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    public class SeanceController : Controller
    {
        private readonly SeanceManager _seanceManager;
        private readonly CinemaEventManager _cinemaEventManager;

        public SeanceController(SeanceManager seanceManager, CinemaEventManager cinemaEventManager)
        {
            _seanceManager = seanceManager;
            _cinemaEventManager = cinemaEventManager;
        }
        

        public IActionResult All([FromQuery]string title)
        {
            if (title == null) 
                return View(_seanceManager.GetAllSeances().OrderBy(x => x.StartingTime).ToList());
            var seances = _seanceManager.FilterSeancesByTitle(title);
            return View(seances.OrderBy(x => x.StartingTime));
        }
        
        public IActionResult Details([FromRoute]int? id)
        {
            if (id is null) return NotFound();
            var seance = _seanceManager.GetSeanceById(id.Value);
            if (seance is null) return NotFound();
            return View(seance);
        }

        public IActionResult Book([FromRoute]int? id)
        {
            if (id == null) return NotFound();
            var seance = _seanceManager.GetSeanceById(id.Value);
            if (seance == null) return NotFound();
            var vm = new BookSeatViewModel
            {
                Seats = _cinemaEventManager.GetSeatsWithTags(seance),
                Seance = seance
            };
            return View(vm);
        }
        

    }
}