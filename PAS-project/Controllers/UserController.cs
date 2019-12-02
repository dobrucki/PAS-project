using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;
using PAS_project.ViewModels;

namespace PAS_project.Controllers
{
    public class UserController: Controller
    {
        private readonly UserManager _userManager;
        private readonly CinemaEventManager _cinemaEventManager;

        public UserController(UserManager userManager, CinemaEventManager cinemaEventManager)
        {
            _userManager = userManager;
            _cinemaEventManager = cinemaEventManager;
        }
        
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        
        [HttpPost] 
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid) return View();
            _userManager.AddUser(user);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult All([FromQuery]string email)
        {
            if (email == null) return View(_userManager.GetAllUsers().ToList());
            var users = _userManager.FilterUsersByEmail(email).ToList();
            return View(users.OrderBy(x => x.Id));
        }
        
        public ActionResult Details([FromRoute]int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            var userDetailsViewModel = new UserDetailsViewModel
            {
                User = user, 
                CinemaEvents = _cinemaEventManager.SearchByUser(user)
            };
            
            return View(userDetailsViewModel);
        }
        

    }
}