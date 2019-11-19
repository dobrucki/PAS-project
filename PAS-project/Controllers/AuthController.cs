using Microsoft.AspNetCore.Mvc;
using PAS_project.Models;

namespace PAS_project.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager _userManager;

        public AuthController(UserManager userManager)
        {
            _userManager = userManager;
        }
    }
}