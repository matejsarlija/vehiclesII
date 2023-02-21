using Vehicles.Repository.Common;
using Vehicles.Service.Models;

namespace Vehicles.Repository;

public interface IVehicleUnitOfWork
{
    IRepository<VehicleMake> VehicleMakeRepository { get; }
    IRepository<VehicleModel> VehicleModelRepository { get; }
    Task<int> SaveChangesAsync();

}