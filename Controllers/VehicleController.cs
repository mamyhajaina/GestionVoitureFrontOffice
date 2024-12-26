using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly NosTragerService _nosTragerService;

        public VehicleController(VehicleService vehicleService, NosTragerService nosTragerService) {
            _vehicleService = vehicleService;
            _nosTragerService = nosTragerService;
        }
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.getAllVehcilesync();
            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.getVehcileByIdAsync(id);
            var nostrager = await _nosTragerService.getAllNosTragersync();
            if (vehicle == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewData["Nostragers"] = nostrager;
            return View(vehicle);
        }
    }
}
