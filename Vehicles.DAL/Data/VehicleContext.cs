using Microsoft.EntityFrameworkCore;
using Vehicles.DAL.Models;

namespace Vehicles.DAL.Data;

public class VehicleContext : DbContext
{
    public VehicleContext(DbContextOptions<VehicleContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleMake> VehicleMake { get; set; } = default!;

    public DbSet<VehicleModel> VehicleModel { get; set; } = default!;
}