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
            var userWithThisEmail = _userManager.GetAllUsers().FirstOrDefault(e => e.Email.Equals(user.Email));
            if (userWithThisEmail != null && userWithThisEmail.Id != user.Id)
            {
                ModelState.AddModelError("Email", "User with this email already exist.");
            }
            var ms = ModelState;
            ms.Remove("PhoneNumber");
            if (!ms.IsValid) return View();
            _userManager.AddUser(user);
            TempData["comment"] = $"Successfully added new user ID: {user.Id}";
            return RedirectToAction("All");
        }
        [HttpGet]
        public ViewResult CreateVip()
        {
            return View();
        }
        
        [HttpPost] 
        public IActionResult CreateVip(User user)
        {
            var userWithThisEmail = _userManager.GetAllUsers().FirstOrDefault(e => e.Email.Equals(user.Email));
            if (userWithThisEmail != null && userWithThisEmail.Id != user.Id)
            {
                ModelState.AddModelError("Email", "User with this email already exist.");
            }
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
            return RedirectToAction("Details", "User", new {id = user.Id});
        }
        
        public ActionResult Deactivate([FromRoute]int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            _userManager.DeActivateUser(user);
            TempData["comment"] = $"Successfully deactivated user with ID: {user.Id}.";
            return RedirectToAction("Details", "User", new {id = user.Id});

        }

        public ActionResult MakeVip([FromRoute] int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            _userManager.MakeVip(user);
            user.PhoneNumber = "Should be changed";
            TempData["comment"] = $"Successfully changed type of user with ID: {user.Id}.";
            return RedirectToAction("Details", "User", new {id = user.Id});
        }
        public ActionResult MakeStandard([FromRoute] int? id)
        {
            if (id is null) return NotFound();
            var user = _userManager.GetUserById(id.Value);
            if (user is null) return NotFound();
            user.PhoneNumber = string.Empty;
            _userManager.MakeStandard(user);
            TempData["comment"] = $"Successfully changed type of user with ID: {user.Id}.";
            return RedirectToAction("Details", "User", new {id = user.Id});
        }
        [HttpGet]
  public ActionResult Edit(int? id)
  {
            
      if (id is null) return NotFound();
      var user = _userManager.GetUserById(id.Value);
      if (user is null) return NotFound();
      var editUser = new EditUserViewModel
      {
          Id = id.Value,
          User = user,
          Type = user.UserType.ToString(),
          Activity = user.Active
      };

      return View(user.UserType.ToString().Equals("Vip") ? "~/Views/User/EditVip.cshtml" : "~/Views/User/EditStandard.cshtml", editUser);
  }
        
  [HttpPost]
  public ActionResult Edit(EditUserViewModel eUser)
  {
      var userWithThisEmail = _userManager.GetAllUsers().FirstOrDefault(e => e.Email.Equals(eUser.User.Email));
      if (userWithThisEmail != null && userWithThisEmail.Id != eUser.Id)
      {
          ModelState.AddModelError("User.Email", "User with this email already exist.");
      }
      if (eUser.Type.Equals("Standard"))
      {
          var ms = ModelState;
          ms.Remove("User.PhoneNumber");
          if (ms.IsValid)
          {

              eUser.User.Id = eUser.Id;
              eUser.User.UserType = Models.Entities.User.StandardUserType;
              eUser.User.Active = eUser.Activity;
              _userManager.UpdateUser(eUser.User);
              
          }
          else
          {
              return View("~/Views/User/EditStandard.cshtml");
          }
      }
      
      else
      {
          if (ModelState.IsValid)
          {

              
              eUser.User.Id = eUser.Id;
              eUser.User.UserType = Models.Entities.User.VipUserType;
              eUser.User.Active = eUser.Activity;
              _userManager.UpdateUser(eUser.User);
              
          }
          else
          {
              return View("~/Views/User/EditVip.cshtml");
          }
      }

      
      return RedirectToAction("Details", new { eUser.Id });
  }
}

}