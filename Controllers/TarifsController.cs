using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class TarifsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
