using Microsoft.EntityFrameworkCore;
using Vehicles.Service.Models;

namespace Vehicles.Service.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext (DbContextOptions<VehicleContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<VehicleModel> VehicleModel { get; set; } = default!;
    }
}
