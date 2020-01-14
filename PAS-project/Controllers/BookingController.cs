using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly CinemaEventManager _cinemaEventManager;
        private readonly UserManager _userManager;
        private readonly SeanceManager _seanceManager;
        private readonly CinemaHallManager _cinemaHallManager;

        public BookingController(
            CinemaEventManager cinemaEventManager, 
            UserManager userManager, 
            SeanceManager seanceManager,
            CinemaHallManager cinemaHallManager)
        {
            _cinemaEventManager = cinemaEventManager;
            _userManager = userManager;
            _seanceManager = seanceManager;
            _cinemaHallManager = cinemaHallManager;
        }

        [HttpPost]
        public IActionResult Create(BookSeatViewModel vm)
        {
            var user = _userManager.GetUserById(vm.UserId);
            var seance = _seanceManager.GetSeanceById(vm.SeanceId);
            var seat = seance.CinemaHall.GetSeatByString(vm.SeatId);
            if (user is null || seance is null || seat is null) return BadRequest();
            var booking = _cinemaEventManager.CreateABooking(user, seance, seat);
            if (booking is null) return BadRequest();
            TempData["comment"] = $"Successfully created reservation with id: {booking.Id}";
            return RedirectToAction("Details", "User", new {id = user.Id});
        }
        
        public IActionResult Cancel([FromRoute]int? id)
        {
            if (!id.HasValue) return BadRequest();
            var evn = _cinemaEventManager.SearchAllBookings().FirstOrDefault(e => e.Id == id.Value);
            if (evn == null) return BadRequest();
            _cinemaEventManager.CancelABooking(_cinemaEventManager.GetEventById(id.Value));
            if (_cinemaEventManager.GetEventById(evn.Id) is null)
            {
                TempData["comment"] = $"Successfully canceled event with id: {evn.Id}";
            }
            else
            {
                TempData["comment"] = $"Could not canceled event with id: {evn.Id}";
            }
            return RedirectToAction("Details", "User", new {id = evn.ApplicationUser.Id});
        }
        
        
        
    }
}