using Microsoft.EntityFrameworkCore;
using Vehicles.MVC.Data;
using Vehicles.MVC.Models;

namespace Vehicles.MVC.Service.VehicleModelRepository;

public class VehicleModelRepository : IVehicleModelRepository
{
    private readonly VehicleContext _context;

    public VehicleModelRepository(VehicleContext context)
    {
        _context = context;
    }
    
    public async Task<IQueryable<VehicleModel>> GetVehicleModelsAsync()
    {
        var vehicleModels = _context.VehicleModel.Include(v => v.VehicleMake);
        return vehicleModels;
    }
}