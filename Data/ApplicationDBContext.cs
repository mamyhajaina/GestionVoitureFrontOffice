using Microsoft.EntityFrameworkCore;

namespace GestionVoitureFrontOffice.Models
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {}

        public DbSet<Mission> Mission { get; set; }

        public DbSet<TragerVehicule> TragerVehicule { get; set; }
    }
}
