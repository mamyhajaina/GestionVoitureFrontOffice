using GestionVoitureFrontOffice.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionVoitureFrontOffice.Pages.Admin
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
