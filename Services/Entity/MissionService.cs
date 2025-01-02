using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionVoitureFrontOffice.Services
{
    public class MissionService : IMissionService
    {
        private readonly ApplicationDBContext _context;

        public MissionService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mission>> GetAllAsync()
        {
            return await _context.Mission.ToListAsync();
        }

        public async Task<Mission> GetByIdAsync(int id)
        {
            return await _context.Mission.FindAsync(id);
        }

        public async Task<Mission> CreateAsync(Mission Mission)
        {
            _context.Mission.Add(Mission);
            await _context.SaveChangesAsync();
            return Mission;
        }

        public async Task<Mission> UpdateAsync(Mission Mission)
        {
            _context.Entry(Mission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Mission;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Mission = await _context.Mission.FindAsync(id);
            if (Mission == null)
                return false;

            _context.Mission.Remove(Mission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
