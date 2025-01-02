using GestionVoitureFrontOffice.Configurations;
using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Sockets;

namespace GestionVoitureFrontOffice.Pages.AdminClient
{
    //[Authorize(Roles = "Client")]
    //[ServiceFilter(typeof(AuthorizeFilter))]
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<Offer> listeOffre { get; set; } = new List<Offer>();

        public IndexModel(ApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var IdClient = _httpContextAccessor.HttpContext?.GetIdUser();

            if (IdClient == null)
            {
                //throw new InvalidOperationException("L'utilisateur n'est pas authentifi� ou IdClient est nul.");
                return RedirectToAction("Index","Login");
            }
            var apiUrl = $"http://localhost:5078/api/OffreAPI/byClient?idClient={IdClient}";
            listeOffre = await _apiService.GetDataFromApiAsync<List<Offer>>(apiUrl);
            return Page();
        }

        public async Task<IActionResult> OnPostValiderOfferAsync(int offerId)
        {
            Console.WriteLine("Valider l'offre avec l'Id : " + offerId);

            // Logique pour valider l'offre (par exemple, mise à jour de son statut dans la base de données)
            // Vous pouvez récupérer l'offre via l'API ou la base de données et effectuer l'action appropriée.

            // Exemple de redirection après la validation
            return RedirectToPage(); // Vous pouvez rediriger vers la même page après la validation
        }
    }
}
