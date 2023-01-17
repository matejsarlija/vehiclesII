using Vehicles.Service.Models;

namespace Vehicles.MVC.ViewModels;

public class VehicleMakeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }
    
    public IEnumerable<VehicleModel> VehicleModels { get; set; }

}