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

        [HttpGet("byClient/{idClient}")]
        public async Task<IActionResult> Get(int idClient)
        {
            List<Offer> listeOffre = await _offreService.getOffreByIdClientAsync(idClient);
            return Json(listeOffre);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Offer> listeOffre = await _offreService.getAllOffreAsync();
            Console.WriteLine("Count: " + listeOffre.Count());
            return Json(listeOffre);
        }

        [HttpGet("NonValider")]
        public async Task<IActionResult> NonValider()
        {
            List<Offer> listeOffre = await _offreService.getAllOffreNonValiderAsync();
            Console.WriteLine("----Count: " + listeOffre.Count());
            return Json(listeOffre);
        }

        [HttpGet("ValiderOffre")]
        public async Task<IActionResult> ValiderOffre(int idOffre)
        {
            bool response = await _offreService.UpdateOffreStatus(idOffre,2);
            return Json(response);
        }

        [HttpGet("TerminerOffre")]
        public async Task<IActionResult> TerminerOffre(int idOffre)
        {
            bool response = await _offreService.UpdateOffreStatus(idOffre, 3);
            return Json(response);
        }
    }
}
