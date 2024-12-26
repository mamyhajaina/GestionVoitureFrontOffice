using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class NosServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
