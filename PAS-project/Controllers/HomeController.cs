using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models;

namespace PAS_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieManager _movieManager;
        private readonly SeanceManager _seanceManager;
        public HomeController(MovieManager movieManager, SeanceManager seanceManager)
        {
            _movieManager = movieManager;
            _seanceManager = seanceManager;
            
        }
        
        // GET
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public ViewResult Movies()
        {
            var movies = _movieManager.GetAllMovies();
            ViewBag.Movies = movies;
            ViewData["Title"] = "Movies";
            return View();
        }

        public ViewResult Seances()
        {
            var seances = _seanceManager.GetAllSeances();
            ViewBag.Seances = seances;
            ViewData["Title"] = "Seances";
            return View();
        }
    }
}