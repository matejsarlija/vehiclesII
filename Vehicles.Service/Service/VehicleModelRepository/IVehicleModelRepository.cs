using Vehicles.Service.Models;

namespace Vehicles.Service.Service.VehicleModelRepository;

public interface IVehicleModelRepository
{
    Task<IQueryable<VehicleModel>> GetVehicleModelsAsync();
    Task<VehicleModel> GetVehicleModelAsync(int? id);
    Task AddVehicleModelAsync(VehicleModel vehicleModel);
    Task UpdateVehicleModelAsync(VehicleModel vehicleModel);
    Task DeleteVehicleModelAsync(int? id);
    Task<bool> VehicleModelExistsAsync(int id);
}