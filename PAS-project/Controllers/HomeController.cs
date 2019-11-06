using Microsoft.AspNetCore.Mvc;

namespace PAS_project.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}