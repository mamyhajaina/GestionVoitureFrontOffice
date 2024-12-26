using System.Diagnostics;
using GestionVoitureFrontOffice.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NavigateToVehicle()
        {
            return RedirectToAction("", "vehicle");
        }

        public IActionResult NotFound()
        {
            return View("_404"); // Assurez-vous que la vue s'appelle "404.cshtml"
        }
    }
}
