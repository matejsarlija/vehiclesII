using Vehicles.Service.Models;

namespace Vehicles.Model.DTO;

public class VehicleMakeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }

    public ICollection<VehicleModel> VehicleModels { get; set; }


}