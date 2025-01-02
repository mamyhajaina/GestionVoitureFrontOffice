using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace GestionVoitureFrontOffice.Services
{
    public class TragerVehiculeService : ITragerVehiculeService
    {
        private readonly ApplicationDBContext _context;

        public TragerVehiculeService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TragerVehicule>> GetAllAsync()
        {
            return await _context.TragerVehicule.ToListAsync();
        }

        public async Task<TragerVehicule> GetByIdAsync(int id)
        {
            return await _context.TragerVehicule.FindAsync(id);
        }

        public async Task<TragerVehicule> CreateAsync(TragerVehicule TragerVehicule)
        {
            _context.TragerVehicule.Add(TragerVehicule);
            await _context.SaveChangesAsync();
            return TragerVehicule;
        }

        public async Task<TragerVehicule> UpdateAsync(TragerVehicule TragerVehicule)
        {
            _context.Entry(TragerVehicule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return TragerVehicule;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Mission = await _context.TragerVehicule.FindAsync(id);
            if (Mission == null)
                return false;

            _context.TragerVehicule.Remove(Mission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TragerVehicule>> GetByIdTragerDepartAndArriverAsync(int idTragerDepart, int idTragerArriver)
        {
            return await _context.TragerVehicule
                .Include(tv => tv.Vehicle)
                .Where(tv => tv.idTragerDepart == idTragerDepart && tv.idTragerArriver == idTragerArriver)
                .ToListAsync();
        }


    }
}
