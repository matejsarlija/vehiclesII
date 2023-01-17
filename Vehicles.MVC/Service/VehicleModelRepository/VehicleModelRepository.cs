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

    public async Task<VehicleModel> GetVehicleModelAsync(int? id)
    {
        return await _context.VehicleModel
            .Include(v => v.VehicleMake)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddVehicleModelAsync(VehicleModel vehicleModel)
    {
        _context.Add(vehicleModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVehicleModelAsync(VehicleModel vehicleModel)
    {
        _context.Update(vehicleModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVehicleModelAsync(int? id)
    {
        var vehicleModel = await _context.VehicleModel
            .Include(v => v.VehicleMake)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (vehicleModel != null)
        {
            _context.VehicleModel.Remove(vehicleModel);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<bool> VehicleModelExistsAsync(int id)
    {
        return await _context.VehicleMake.AnyAsync(v => v.Id == id);
    }
}