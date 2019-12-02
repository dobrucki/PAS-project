using System.Linq;
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
        
        public ActionResult All(string email=null)
        {
            var users = _userManager.GetAllUsers().ToList();
            if (!string.IsNullOrEmpty(email)) users = users
                .Where(u => u.Email.ToLower().Contains(email.ToLower())).ToList();            
            if (users.Any())
            {
                return View(users.OrderBy(x => x.Id));
            }
            return NotFound();
        }
        
        public ActionResult Details(int id)
        {
            var user = _userManager.GetUserById(id);
            var userDetailsViewModel = new UserDetailsViewModel
                {User = user, CinemaEvents = _cinemaEventManager.SearchByUser(user)};
            if (user is null)
            {
                return NotFound();
            }
            
            return View(userDetailsViewModel);
        }
        

    }
}