using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View("_404"); // Assurez-vous que la vue s'appelle "404.cshtml"
        }
    }
}
