using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    public class SeanceController : Controller
    {
        private readonly SeanceManager _seanceManager;
        private readonly CinemaEventManager _cinemaEventManager;
        private readonly MovieManager _movieManager;
        private readonly CinemaHallManager _cinemaHallManager;

        public SeanceController(SeanceManager seanceManager, 
            CinemaEventManager cinemaEventManager,
            MovieManager movieManager,
            CinemaHallManager cinemaHallManager)
        {
            _seanceManager = seanceManager;
            _cinemaEventManager = cinemaEventManager;
            _movieManager = movieManager;
            _cinemaHallManager = cinemaHallManager;
        }


        public IActionResult All([FromQuery] string title)
        {
            if (title == null)
                return View(_seanceManager.GetAllSeances().OrderBy(x => x.StartingTime).ToList());
            var seances = _seanceManager.FilterSeancesByTitle(title);
            return View(seances.OrderBy(x => x.StartingTime));
        }

        public IActionResult Details([FromRoute] int? id)
        {
            if (id is null) return NotFound();
            var seance = _seanceManager.GetSeanceById(id.Value);
            if (seance is null) return NotFound();
            return View(seance);
        }

        public IActionResult Book([FromRoute] int? id)
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddSeanceViewModel vm)
        {
            var movie = _movieManager.GetMovieById(vm.MovieId);
            if (movie == null)
                ModelState.AddModelError("MovieId", "Given movie does not exist.");
            var cinemaHall = _cinemaHallManager.GetCinemaHallById(vm.CinemaHallId);
            if (cinemaHall == null)
                ModelState.AddModelError("CinemaHallId", "Given CinemaHall does not exist.");
            DateTime? datetime = null;
            try
            {
                datetime = Convert.ToDateTime(vm.DateTime);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("DateTime", "Time is not formatted correctly.");
            }

            if (datetime != null && DateTime.Now >= datetime)
            {
                ModelState.AddModelError("DateTime", "Selected time is from the past.");
            }

            if (!ModelState.IsValid)
                return View();

            var seance = new Seance
            {
                Movie = movie,
                CinemaHall = cinemaHall,
                StartingTime = datetime.Value
            };
            try
            {
                _seanceManager.AddSeance(seance);
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError("DateTime", $"{exception.Message}");
            }

            if (!ModelState.IsValid) return View();

            TempData["comment"] = $"Successfully added seance with id: {seance.Id}";
            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null) return BadRequest();
            var seance = _seanceManager.GetSeanceById(id.Value);
            if (seance is null) return BadRequest();

            var vm = new EditSeanceViewModel
            {
                Seance = seance,
                Id = seance.Id,
                CinemaHallId = seance.CinemaHall.Id,
                MovieId = seance.Movie.Id,
                DateTime = seance.StartingTime.ToString()
            };
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditSeanceViewModel vm)
        {
            var seance = _seanceManager.GetSeanceById(vm.Id);
            if (seance == null) return BadRequest();
            
            var movie = _movieManager.GetMovieById(vm.MovieId);
            if (movie == null)
                ModelState.AddModelError("MovieId", "Given movie does not exist.");
            var cinemaHall = _cinemaHallManager.GetCinemaHallById(vm.CinemaHallId);
            if (cinemaHall == null)
                ModelState.AddModelError("CinemaHallId", "Given CinemaHall does not exist.");
            DateTime? datetime = null;
            try
            {
                datetime = Convert.ToDateTime(vm.DateTime);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("DateTime", "Time is not formatted correctly.");
            }

            if (datetime != null && DateTime.Now >= datetime)
            {
                ModelState.AddModelError("DateTime", "Selected time is from the past.");
            }

            if (!ModelState.IsValid)
                return View();

            var seanceUpdate = new Seance
            {
                Id = vm.Id,
                CinemaHall = cinemaHall,
                Movie = movie,
                StartingTime = datetime.Value
            };
            try
            {
                _seanceManager.UpdateSeance(seanceUpdate);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) return View();

            TempData["comment"] = $"Successfully updated seance with id: {seance.Id}";
            return RedirectToAction("All");
        }

        public IActionResult Delete(int id)
        {
            var seance = _seanceManager.GetSeanceById(id);
            if (seance is null)
            {
                TempData["comment"] = $"Can not find seance with given id: {id}";
            }
            else
            {
                _seanceManager.DeleteSeance(seance);
                TempData["comment"] = $"Successfully deleted seance with given id: {seance.Id}";
            }
            return RedirectToAction("All", "Seance");
        }
        

    }
}