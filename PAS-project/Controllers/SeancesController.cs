using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PAS_project.Models.Managers;

namespace PAS_project.Controllers
{
    public class SeancesController : Controller
    {
        private readonly SeanceManager _seanceManager;

        public SeancesController(SeanceManager seanceManager)
        {
            _seanceManager = seanceManager;
        }
        // GET
        

        public ActionResult All(string title=null)
        {
            var seances = _seanceManager.GetAllSeances().ToList();
            if (!string.IsNullOrEmpty(title)) seances = seances
                .Where(s => s.Movie.Title.ToLower().Contains(title.ToLower())).ToList();
            if (seances.Any())
            {
                return View(seances.OrderBy(x => x.StartingTime));
            }
            return NotFound();
        }
        
        public ActionResult Seance(int id)
        {
            var seance = _seanceManager.GetSeanceById(id);
            if (seance is null)
            {
                return NotFound();
            }
            return View(seance);
        }

    }
}