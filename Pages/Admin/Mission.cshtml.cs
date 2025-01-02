using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    public class MissionModel : PageModel
    {
        public string UserEmail { get; private set; } = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MissionModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            UserEmail = _httpContextAccessor.HttpContext.GetUserEmail();
        }
    }
}
