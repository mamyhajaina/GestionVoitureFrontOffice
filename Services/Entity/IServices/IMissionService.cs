
using GestionVoitureFrontOffice.Models;

namespace GestionVoitureFrontOffice.Services.IServices
{
    public interface IMissionService
    {
        Task<IEnumerable<Mission>> GetAllAsync();
        Task<Mission> GetByIdAsync(int id);
        Task<Mission> CreateAsync(Mission Mission);
        Task<Mission> UpdateAsync(Mission Mission);
        Task<bool> DeleteAsync(int id);
    }
}