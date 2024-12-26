using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class AproposController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
