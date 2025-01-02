using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace GestionVoitureFrontOffice.Models
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Offer")]
        public int offerId { get; set; }
        public Offer Offer { get; set; }

        [ForeignKey("Vehicle")]
        public int idVehicle { get; set; }
        public Vehicle Vehicle { get; set; }

        [Column(TypeName = "int")]
        [DisplayName("Nombre de voyage")]
        public int NumberTrips { get; set; }

        [Column(TypeName = "bit")]
        [DisplayName("Si true alors de mission est terminé alors si false la mission est refuser ou annuler")]
        public bool status { get; set; }

        [Column(TypeName = "datetime")]
        [DisplayName("Créé le")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        [DisplayName("Mis à jour le")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
