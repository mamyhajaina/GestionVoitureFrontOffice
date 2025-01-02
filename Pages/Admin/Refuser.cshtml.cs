using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    public class RefuserModel : PageModel
    {
        public readonly OffreService _offreService;
        public bool resulteDelete = false;
        public string UserEmail { get; private set; } = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RefuserModel(OffreService offreService, IHttpContextAccessor httpContextAccessor)
        {
            _offreService = offreService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGetAsync(int IdOffre)
        {
            UserEmail = _httpContextAccessor.HttpContext.GetUserEmail();
            resulteDelete = await _offreService.DeleteOfferByIdAsync(IdOffre);
            return Page();
        }
    }
}
