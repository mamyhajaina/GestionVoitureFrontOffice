using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionVoitureFrontOffice.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? NameSociete { get; set; }
        public int? IdTragerDeparture { get; set; }
        public NosTrager? TragerDeparture { get; set; }
        public int? IdTragerArriving { get; set; }
        public NosTrager? TragerArriving { get; set; }
        public int? IdClient { get; set; }
        public User? Client { get; set; }
        public int? IdVehicle { get; set; }
        public Vehicle? Vehicle { get; set; }
        public string? OtherTragerDescription { get; set; }

        [StringLength(2, ErrorMessage = "La description ne doit pas dépasser 255 caractères.")]
        public string Description { get; set; }
        public DateTime DateMission { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal? Capacity { get; set; }
    }

}
