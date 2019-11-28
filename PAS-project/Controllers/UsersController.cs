using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;

namespace PAS_project.Controllers
{
    public class UsersController: Controller
    {
        private readonly UserManager _userManager;

        public UsersController(UserManager userManager)
        {
            _userManager = userManager;
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
            if (!string.IsNullOrEmpty(email)) users = users.Where(u => u.Email == email).ToList();
            if (users.Any())
            {
                return View(users.OrderBy(x => x.LastName));
            }
            return NotFound();
        }
        
        public ActionResult User(int id)
        {
            var user = _userManager.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }
            return View(user);
        }
        

    }
}