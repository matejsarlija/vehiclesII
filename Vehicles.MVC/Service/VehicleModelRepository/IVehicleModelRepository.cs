using Vehicles.MVC.Models;

namespace Vehicles.MVC.Service.VehicleModelRepository;

public interface IVehicleModelRepository
{
    Task<IQueryable<VehicleModel>> GetVehicleModelsAsync();
    Task<VehicleModel> GetVehicleModelAsync(int? id);
}