using Vehicles.Service.Models;

namespace Vehicles.Repository.Common;

public interface IVehicleUnitOfWork
{
    IRepository<VehicleMake> VehicleMakeRepository { get; }
    IRepository<VehicleModel> VehicleModelRepository { get; }
    Task<int> SaveChangesAsync();

}