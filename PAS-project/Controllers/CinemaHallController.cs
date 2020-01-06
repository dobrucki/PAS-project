using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    public class CinemaHallController : Controller
    {
        private readonly CinemaHallManager _cinemaHallManager;
        private readonly CinemaEventManager _cinemaEventManager;

        public CinemaHallController(CinemaHallManager cinemaHallManager, CinemaEventManager cinemaEventManager)
        {
            _cinemaHallManager = cinemaHallManager;
            _cinemaEventManager = cinemaEventManager;
        }

        public IActionResult All()
        {
            var cinemaHalls = _cinemaHallManager.GetAllCinemaHalls();
            return View(cinemaHalls);
        }

        public IActionResult Details([FromRoute] int? id)
        {
            if (id is null) return NotFound();
            var cinemaHall = _cinemaHallManager.GetCinemaHallById(id.Value);
            if (cinemaHall is null) return NotFound();
            return View(cinemaHall);
        }
    }
}