using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services.IServices;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    public class ValidationModel : PageModel
    {
        public List<Vehicle> listeVoitureDispo { get; set; } = new List<Vehicle>();
        public List<TragerVehicule> listeTrager { get; set; } = new List<TragerVehicule>();
        private readonly ITragerVehiculeService _itragerVehiculeService;
        public string UserEmail { get; private set; } = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ValidationModel(ITragerVehiculeService itragerVehiculeService, IHttpContextAccessor httpContextAccessor)
        {
            _itragerVehiculeService = itragerVehiculeService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGetAsync(int idDepart, int idArriver)
        {
            UserEmail = _httpContextAccessor.HttpContext.GetUserEmail();
            if (idDepart>=0 && idArriver>=0)
            {
                listeTrager = await _itragerVehiculeService.GetByIdTragerDepartAndArriverAsync(idDepart, idArriver);
                
            }
            Console.WriteLine("------listeTrage-------" + listeTrager.Count());
            return Page();
        }
    }
}
