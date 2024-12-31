using GestionVoitureFrontOffice.Configurations;
using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionVoitureFrontOffice.Controllers
{
    [ServiceFilter(typeof(AuthorizeFilter))]
    public class OffreController : Controller
    {
        public readonly OffreService _offreService;
        private readonly TragerService _nosTragerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OffreController(OffreService offreService, TragerService nosTragerService, IHttpContextAccessor httpContextAccessor)
        {
            _offreService = offreService;
            _nosTragerService = nosTragerService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(int IdVehicle)
        {
            List<NosTrager> nostragerDepart = null;
            List<NosTrager> nostragerArriver = null;
            if (IdVehicle > 0)
            {
                nostragerDepart = await _nosTragerService.GetByIdVehicleDepartVehicleAsync(IdVehicle);
                nostragerArriver = await _nosTragerService.GetByIdVehicleArriverVehicleAsync(IdVehicle);
            }
            else
            {
                var notrager = await _nosTragerService.getAllNosTragersync();
                nostragerDepart = notrager;
                nostragerArriver = notrager;
            }
            ViewData["nostragerDepart"] = nostragerDepart;
            ViewData["nostragerArriver"] = nostragerArriver;
            ViewData["IdVehicle"] = IdVehicle;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DemandeOffre([Bind("Email", "NameSociete", "IdTragerDeparture", "IdTragerArriving", "IdVehicle", "Description", "Capacity")] Offer offerData)
        {
            offerData.status = 0;
            if (!ModelState.IsValid)
            {
                Console.WriteLine("---------ModelState.IsValid a échoué");
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Erreur : {modelError.ErrorMessage} {modelError.Exception}");
                }
                return RedirectToAction("Index", offerData);
            }

            if (DateTime.TryParse(Request.Form["DateMission"], out DateTime dateMission))
            {
                Console.WriteLine("Request.Form[: " + Request.Form["DateMission"]);
                offerData.DateMission = dateMission;
            }

            if (offerData.DateMission == DateTime.MinValue || offerData.DateMission < new DateTime(1753, 1, 1))
            {
                return BadRequest("Invalid DateMission value. " + offerData.DateMission);
            }
            if (offerData.IdVehicle.HasValue && offerData.IdTragerDeparture.HasValue && offerData.IdTragerArriving.HasValue)
            {
                decimal defaultLocation = await _nosTragerService.searcheVehicleLocationWithTragerAsync(
                    offerData.IdVehicle.Value,
                    offerData.IdTragerDeparture.Value,
                    offerData.IdTragerArriving.Value
                );
                offerData.status = 1;
                offerData.TotalAmount = _offreService.calculeMontant(defaultLocation, (decimal)offerData.Capacity);
            }
            else
            {
                Console.WriteLine("Une ou plusieurs valeurs sont nulles. Impossible de calculer le montant.");
            }

            Console.WriteLine("-------------offerData.IdVehicle " + offerData.IdVehicle);
            offerData.IdClient = _httpContextAccessor.HttpContext?.GetIdUser();
            var responseInsert = await _offreService.AddOffre(offerData);
            Console.WriteLine("responseInsert: " + responseInsert);
            return RedirectToAction("ListeOffre");
        }

        public async Task<IActionResult> ListeOffre()
        {
            var IdClient = _httpContextAccessor.HttpContext?.GetIdUser();
            List<Offer> offers = await _offreService.getOffreByIdClientAsync(IdClient);
            Console.WriteLine("Count liste array: " + offers.Count);
            return View(offers);
        }
    }
}
