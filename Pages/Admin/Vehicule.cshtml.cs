using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    public class VehiculeModel : PageModel
    {
        public string UserEmail { get; private set; } = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VehiculeModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            UserEmail = _httpContextAccessor.HttpContext.GetUserEmail();
        }
    }
}
