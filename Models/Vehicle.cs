namespace GestionVoitureFrontOffice.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string? Pseudo { get; set; }
        public int TypeVehicleId { get; set; }
        public TypeVehicle TypeVehicle { get; set; }
        public string Brand { get; set; }
        public string? Model { get; set; }
        public int Capacity { get; set; }
        public string Descriptions { get; set; }
        public int TableKilometer { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}
