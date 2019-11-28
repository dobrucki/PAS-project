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
        private readonly SeanceManager _seanceManager;

        public HomeController(MovieManager movieManager, SeanceManager seanceManager, UserManager userManager)
        {
            _movieManager = movieManager;
            _seanceManager = seanceManager;
            _userManager = userManager;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
    }
}