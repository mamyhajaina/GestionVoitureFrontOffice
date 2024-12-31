using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class OffreAPIController(OffreService offreService) : Controller
    {
        public readonly OffreService _offreService = offreService ?? throw new ArgumentNullException(nameof(offreService));

        [HttpGet]
        public async Task<IActionResult> Get(int idClient)
        {
            List<Offer> listeOffre = await _offreService.getOffreByIdClientAsync(idClient);
            return Json(listeOffre);
        }
    }
}
