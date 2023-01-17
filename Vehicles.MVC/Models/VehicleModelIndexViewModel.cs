using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vehicles.MVC.Models;

public class VehicleModelIndexViewModel
{
    public PaginatedList<VehicleModel> VehicleModels { get; set; }
    public SelectList VehicleMakes { get; set; }
    public string VehicleModelMake { get; set; }
    public string SearchString { get; set; }
}