using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicles.MVC.Models;

namespace Vehicles.MVC.Data
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
