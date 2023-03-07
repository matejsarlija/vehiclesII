using Vehicles.DAL.Data;
using Vehicles.DAL.Models;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;

public class VehicleUnitOfWork : IVehicleUnitOfWork
{
    private readonly VehicleContext _context;
    private IRepository<VehicleMake> _vehicleMakeRepository;
    private IRepository<VehicleModel> _vehicleModelRepository;

    public VehicleUnitOfWork(VehicleContext context)
    {
        _context = context;
    }


    public IRepository<VehicleMake> VehicleMakeRepository
    {
        get
        {
            if (_vehicleMakeRepository == null) _vehicleMakeRepository = new GenericRepository<VehicleMake>(_context);

            return _vehicleMakeRepository;
        }
    }

    public IRepository<VehicleModel> VehicleModelRepository
    {
        get
        {
            if (_vehicleModelRepository == null)
                _vehicleModelRepository = new GenericRepository<VehicleModel>(_context);

            return _vehicleModelRepository;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}