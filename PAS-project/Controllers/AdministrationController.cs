using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Entities;

namespace PAS_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdministrationController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        
        //public IActionResult 
    }
}