using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly TragerService _nosTragerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VehicleController(VehicleService vehicleService, TragerService nosTragerService, IHttpContextAccessor httpContextAccessor)
        {
            _vehicleService = vehicleService;
            _nosTragerService = nosTragerService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index(string? textSearched, int? page = 1, int? pageSize = 6)
        {
            var userEmail = _httpContextAccessor.HttpContext?.GetUserEmail();
            var userId = _httpContextAccessor.HttpContext?.GetIdUser();
            var userRole = _httpContextAccessor.HttpContext?.GetUserRole();
            Console.WriteLine("+++++++++++UserEmail: " + userEmail + " ID: " + userId + " Role: " + userRole);

            Console.WriteLine("textSearched: " + textSearched + " page: " + page + " pageSize: " + pageSize);

            var result = await _vehicleService.ShearchVehiclesAsync(textSearched, page, pageSize);
            var nostrager = await _nosTragerService.getAllNosTragersync();
            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalCount"] = result.TotalCount;
            ViewData["TextSearched"] = textSearched;

            return View(result.Vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            Offer offer = new Offer();
            var vehicle = await _vehicleService.getVehcileByIdAsync(id);
            var nostrager = await _nosTragerService.getAllNosTragersync();
            if (vehicle == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewData["Nostragers"] = nostrager;
            ViewData["Offer"] = offer;
            return View(vehicle);
        }
    }
}
