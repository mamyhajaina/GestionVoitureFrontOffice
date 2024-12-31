using GestionVoitureFrontOffice.Configurations;
using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.AdminClient
{
    //[Authorize(Roles = "Client")]
    [ServiceFilter(typeof(AuthorizeFilter))]
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;
        public List<Offer> listeOffre { get; set; }

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7263/api/OffreAPI";
            listeOffre = await _apiService.GetDataFromApiAsync<List<Offer>>(apiUrl);
        }
    }
}
