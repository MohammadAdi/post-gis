using Microsoft.EntityFrameworkCore;
using postgis.Models;
using System.Threading;
using System.Threading.Tasks;

namespace postgis.Infrastructures.Context
{

    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<LocationPoint> LocationPoints { get; set; }
        DbSet<LocationPolygon> LocationPolygons { get; set; }
        DbSet<LocationPolyLine> LocationPolyLines { get; set; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = new CancellationToken());
    }

    public class ApplicationDBContext : DbContext,IApplicationDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<LocationPoint> LocationPoints { get; set; }
        public DbSet<LocationPolygon> LocationPolygons { get; set; }
        public DbSet<LocationPolyLine> LocationPolyLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.HasPostgresExtension("postgis");

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
