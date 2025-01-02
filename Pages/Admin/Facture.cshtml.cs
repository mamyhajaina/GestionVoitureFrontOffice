using GestionVoitureFrontOffice.Services.IServices;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    public class FactureModel : PageModel
    {
        public string UserEmail { get; private set; } = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FactureModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            UserEmail = _httpContextAccessor.HttpContext.GetUserEmail();
        }
    }
}
