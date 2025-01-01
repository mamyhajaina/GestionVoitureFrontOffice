using GestionVoitureFrontOffice.Configurations;
using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Sockets;

namespace GestionVoitureFrontOffice.Pages.AdminClient
{
    //[Authorize(Roles = "Client")]
    [ServiceFilter(typeof(AuthorizeFilter))]
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

        public async Task OnGetAsync()
        {
            var IdClient = _httpContextAccessor.HttpContext?.GetIdUser();

            if (IdClient == null)
            {
                throw new InvalidOperationException("L'utilisateur n'est pas authentifié ou IdClient est nul.");
            }
            var apiUrl = $"https://localhost:7263/api/OffreAPI?idClient={IdClient}";
            listeOffre = await _apiService.GetDataFromApiAsync<List<Offer>>(apiUrl);
        }
    }
}
