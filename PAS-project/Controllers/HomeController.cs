using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models;
using PAS_project.Models.Entities;
using PAS_project.Models.Managers;

namespace PAS_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieManager _movieManager;
        private readonly UserManager _userManager;
        public HomeController(MovieManager movieManager, UserManager userManager)
        {
            _movieManager = movieManager;
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies()
        {
            var movies = _movieManager.GetAllMovies();
            return View(movies);
        }

        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }
        
        [HttpPost] 
        public IActionResult SignUp(User user)
        {
            if (!ModelState.IsValid) return View();
            _userManager.AddUser(user);
            return RedirectToAction("Movies");

        }
    }
}