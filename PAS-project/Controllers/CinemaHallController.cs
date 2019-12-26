using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Managers;

namespace PAS_project.Controllers
{
    public class CinemaHallController : Controller
    {
        private readonly CinemaHallManager _cinemaHallManager;

        public CinemaHallController(CinemaHallManager cinemaHallManager)
        {
            _cinemaHallManager = cinemaHallManager;
        }

        public IActionResult All()
        {
            var cinemaHalls = _cinemaHallManager.GetAllCinemaHalls();
            return View(cinemaHalls);
        }
    }
}