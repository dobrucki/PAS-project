using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    [Authorize]
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
            var vm = new EditCinemaHallViewModel {CinemaHall = cinemaHall, SeatId = null};
            return View(vm);
        }

      
        [HttpGet]
        public IActionResult Edit(int cinemaHallId, string seatId)
        {
            if (!string.IsNullOrEmpty(seatId) && cinemaHallId > 50000)
            {
                var cinemaHall = _cinemaHallManager.GetCinemaHallById(cinemaHallId);
                var seat = cinemaHall.GetSeatByString(seatId);
                if (cinemaHall != null && seat != null)
                {
                    return View(new EditSeatViewModel {CinemaHall = cinemaHall, Seat = seat});
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(EditSeatViewModel vm)
        {
            
                var cinemaHall = _cinemaHallManager.GetCinemaHallById(vm.CinemaHallId);
                var seat = cinemaHall.GetSeatByRowColumn(vm.SeatRow, vm.SeatColumn);
                if (cinemaHall != null && seat != null)
                {
                    seat.PlainComment = vm.PlainComment;
                    if (seat.GetType() == typeof(CinemaHall.WideSeat))
                    {
                        var wideSeat = (CinemaHall.WideSeat) seat;
                        wideSeat.ExtendedComment = vm.ExtendedComment;
                    }

                    return RedirectToAction("Details", new {cinemaHall.Id});
                }
            
                return NotFound();
        }
    }
}