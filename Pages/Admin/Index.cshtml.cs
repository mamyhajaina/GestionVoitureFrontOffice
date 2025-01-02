using GestionVoitureFrontOffice.Configurations;
using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using GestionVoitureFrontOffice.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    public class IndexModel : PageModel
    {

        private readonly ApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMissionService _missionService;
        private Offer offer { get; set; }
        public List<Offer> listeOffre { get; set; } = new List<Offer>();
        public string UserEmail { get; private set; } = string.Empty;

        public IndexModel(ApiService apiService, IHttpContextAccessor httpContextAccessor, IMissionService missionService)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
            _missionService = missionService;
        }

        public async Task<IActionResult> OnPostValiderOffer(int offerId)
        {
            Console.WriteLine("Valider l'offre avec l'Id : " + offerId);

            if (offerId == 0)
            {
                Console.WriteLine("L'ID de l'offre est invalide.");
            }

            // Logique de validation ici

            return RedirectToPage(); // Redirige vers la même page après la validation
        }



        public async Task<IActionResult> OnGetAsync(int offerId, int IdVehicle)
        {
            try
            {
                var IdAdmin = _httpContextAccessor.HttpContext?.GetIdUser();
                var RoleAdmin = _httpContextAccessor.HttpContext.GetUserRole();
                UserEmail = _httpContextAccessor.HttpContext.GetUserEmail();

                Console.WriteLine("Get Valider l'offre avec l'Id : " + IdVehicle);
                Console.WriteLine("OnGetAsync++++");

                if (offerId>0 && IdVehicle>0)
                {

                }
                Console.WriteLine("-----IdAdmin: " + IdAdmin + " RoleAdmin: " + RoleAdmin);
                if (IdAdmin == null && RoleAdmin != "Admin")
                {
                    return RedirectToAction("Index", "Login");
                }
                var apiUrl = $"http://localhost:5078/api/OffreAPI/NonValider";
                listeOffre = await _apiService.GetDataFromApiAsync<List<Offer>>(apiUrl);
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine("---Erreur : " + ex.Message);
                throw new Exception("Une erreur est survenue lors de la récupération des offres.", ex);
            }
        }

    }
}
