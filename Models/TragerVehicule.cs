using System.ComponentModel.DataAnnotations;

namespace GestionVoitureFrontOffice.Models
{
    public class TragerVehicule
    {
        public int Id { get; set; }
        public int idVehicle { get; set; }
        public Vehicle Vehicle { get; set; }
        public int idTragerDepart { get; set; }
        public NosTrager TragerDepart { get; set; }
        public int idTragerArriver { get; set; }
        public NosTrager TragerArriver { get; set; }
        public decimal prixLocation { get; set; }

    }
}
