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
        public OffreController(OffreService offreService) {
            _offreService = offreService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DemandeOffre([Bind("Email", "NameSociete", "IdTragerDeparture", "IdTragerArriving", "IdVehicle", "OtherTragerDescription", "Description", "TotalAmount", "Capacity")] Offer offerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid form data.");
            }
            offerData.IdClient = null;
            Console.WriteLine("Offre: " + offerData);
            //var offre = new Offer
            //{
            //    IdTragerDeparture = formData.ContainsKey("placeDeparatureId") && int.TryParse(formData["placeDeparatureId"]?.ToString(), out var placeDeparatureId) ? placeDeparatureId : (int?)null,
            //    IdTragerArriving = formData.ContainsKey("placeArrivingId") && int.TryParse(formData["placeArrivingId"]?.ToString(), out var placeArrivingId) ? placeArrivingId : (int?)null,
            //    IdClient = null, // Vous pouvez ajouter une logique similaire pour `IdClient` si nécessaire
            //    IdVehicle = formData.ContainsKey("IdVehicle") && int.TryParse(formData["IdVehicle"]?.ToString(), out var idVehicle) ? idVehicle : (int?)null,
            //    OtherTragerDescription = "", // Ou utilisez `formData["OtherTragerDescription"]?.ToString() ?? ""` si vous avez une clé correspondante
            //    Description = formData.ContainsKey("descriptions") ? formData["descriptions"]?.ToString() : "",
            //    DateMission = formData.ContainsKey("dateMission") && DateTime.TryParse(formData["dateMission"]?.ToString(), out var dateMission) ? dateMission : DateTime.Now,
            //    TotalAmount = formData.ContainsKey("totalAmount") && decimal.TryParse(formData["totalAmount"]?.ToString(), out var totalAmount) ? totalAmount : 0,
            //    Capacity = formData.ContainsKey("capacity") && decimal.TryParse(formData["capacity"]?.ToString(), out var capacity) ? capacity : 0
            //};

            //Console.WriteLine("---offre: " + offre);
            //var responseInsert = await _offreService.AddOffre(offre);
            //Console.WriteLine("responseInsert: " + responseInsert);
            return View();
        }
    }
}
