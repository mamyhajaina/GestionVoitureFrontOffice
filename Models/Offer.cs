using System.ComponentModel.DataAnnotations;

namespace GestionVoitureFrontOffice.Models
{
    public class Offer
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Veuillez fournir une adresse email valide.")]
        public string? Email { get; set; }
        [StringLength(255, ErrorMessage = "Le nom de la société ne doit pas dépasser 255 caractères.")]
        public string? NameSociete { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un transporteur de départ valide.")]
        public int? IdTragerDeparture { get; set; }
        public NosTrager? TragerDeparture { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un transporteur d'arrivée valide.")]
        public int? IdTragerArriving { get; set; }
        public NosTrager? TragerArriving { get; set; }
        public int? IdClient { get; set; }
        public User? Client { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un véhicule valide.")]
        public int? IdVehicle { get; set; }
        public Vehicle? Vehicle { get; set; }
        public string? OtherTragerDescription { get; set; }

        [StringLength(254, ErrorMessage = "La description ne doit pas dépasser 255 caractères.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "La date de la mission est obligatoire.")]
        public DateTime DateMission { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Le montant total doit être un nombre positif.")]
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "La capacité doit être un nombre positif.")]
        public decimal? Capacity { get; set; }
        public int? status { get; set; }
        public string? statusName { get; set; }
    }

}
