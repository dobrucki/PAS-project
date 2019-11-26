using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models;
using PAS_project.Models.Managers;

namespace PAS_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieManager _movieManager;
        public HomeController(MovieManager movieManager)
        {
            _movieManager = movieManager;
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
    }
}