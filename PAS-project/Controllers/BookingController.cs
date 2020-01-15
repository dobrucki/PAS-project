using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    [Authorize(Roles="User,Admin")]
    public class BookingController : Controller
    {
        private readonly CinemaEventManager _cinemaEventManager;
        private readonly SeanceManager _seanceManager;
        private readonly CinemaHallManager _cinemaHallManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(
            CinemaEventManager cinemaEventManager, 
            SeanceManager seanceManager,
            CinemaHallManager cinemaHallManager, 
            UserManager<ApplicationUser> userManager)
        {
            _cinemaEventManager = cinemaEventManager;
            _seanceManager = seanceManager;
            _cinemaHallManager = cinemaHallManager;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Create(BookSeatViewModel vm)
        {
            var user = _userManager.GetUserAsync(User).Result;
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