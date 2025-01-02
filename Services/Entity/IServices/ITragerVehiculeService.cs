
using GestionVoitureFrontOffice.Models;

namespace GestionVoitureFrontOffice.Services.IServices
{
    public interface ITragerVehiculeService
    {
        Task<IEnumerable<TragerVehicule>> GetAllAsync();
        Task<TragerVehicule> GetByIdAsync(int id);
        Task<TragerVehicule> CreateAsync(TragerVehicule TragerVehicule);
        Task<TragerVehicule> UpdateAsync(TragerVehicule TragerVehicule);
        Task<bool> DeleteAsync(int id);
        Task<List<TragerVehicule>> GetByIdTragerDepartAndArriverAsync(int idTragerDepart, int idTragerArriver);
    }
}