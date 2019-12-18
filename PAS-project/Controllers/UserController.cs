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
        public ViewResult CreateStandard()
        {
            return View();
        }
        
        [HttpPost] 
        public IActionResult CreateStandard(User user)
        {
            var ms = ModelState;
            ms.Remove("PhoneNumber");
            if (!ms.IsValid) return View();
            _userManager.AddUser(user);
            TempData["comment"] = $"Successfully added new user ID: {user.Id}";
            return RedirectToAction("All");
        }
        public ViewResult CreateVip()
        {
            return View();
        }
        
        [HttpPost] 
        public IActionResult CreateVip(User user)
        {
            if (!ModelState.IsValid) return View();
            user.UserType = Models.Entities.User.VipUserType;
            _userManager.AddUser(user);
            TempData["comment"] = $"Successfully added new user ID: {user.Id}";
            return RedirectToAction("All");
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

        public ActionResult Activate([FromRoute]int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            _userManager.ActivateUser(user);
            TempData["comment"] = $"Successfully activated user with ID: {user.Id}.";
            return RedirectToAction("All");
        }
        
        public ActionResult Deactivate([FromRoute]int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            _userManager.DeActivateUser(user);
            TempData["comment"] = $"Successfully deactivated user with ID: {user.Id}.";
            return RedirectToAction("All");

        }

        public ActionResult MakeVip([FromRoute] int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            _userManager.MakeVip(user);
            user.PhoneNumber = "Should be changed";
            TempData["comment"] = $"Successfully changed type of user with ID: {user.Id}.";
            return RedirectToAction("All");
        }
        public ActionResult MakeStandard([FromRoute] int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            user.PhoneNumber = string.Empty;
            _userManager.MakeStandard(user);
            TempData["comment"] = $"Successfully changed type of user with ID: {user.Id}.";
            return RedirectToAction("All");
        }
  
        [HttpGet]
        public ActionResult EditStandard(int? id)
        {
            
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            var editUser = new EditUserViewModel
            {
                Id = id.Value,
                User = user
            };

            return View(editUser);
        }
        
        [HttpPost]
        public ActionResult EditStandard(EditUserViewModel eUser)
        {
            var ms = ModelState;
                ms.Remove("User.PhoneNumber");
                if (!ms.IsValid)
                {

                    return View();
                }

            eUser.User.Id = eUser.Id;
            _userManager.UpdateUser(eUser.User);
            return RedirectToAction("Details", new { eUser.Id });
        }
        
          
        [HttpGet]
        public ActionResult EditVip(int? id)
        {
            
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            var editUser = new EditUserViewModel
            {
                Id = id.Value,
                User = user,
            };
            return View(editUser);

        }
        
        [HttpPost]
        public ActionResult EditVip(EditUserViewModel editUser)
        {

            if (!ModelState.IsValid)
            {

                return View();
            }
            editUser.User.Id = editUser.Id;
            editUser.User.UserType = Models.Entities.User.VipUserType;
            _userManager.UpdateUser(editUser.User);
            return RedirectToAction("Details", new { editUser.Id });
        }
        
}

}