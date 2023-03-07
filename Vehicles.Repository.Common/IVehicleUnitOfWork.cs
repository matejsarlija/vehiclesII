using Vehicles.DAL.Models;

namespace Vehicles.Repository.Common;

public interface IVehicleUnitOfWork
{
    IRepository<VehicleMake> VehicleMakeRepository { get; }
    IRepository<VehicleModel> VehicleModelRepository { get; }
    Task<int> SaveChangesAsync();

}