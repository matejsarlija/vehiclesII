using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicles.Service.Helpers;
using Vehicles.Service.Models;

namespace Vehicles.MVC.ViewModels;

public class VehicleModelIndexViewModel
{
    public PaginatedList<VehicleModelViewModel> VehicleModels { get; set; }
    public SelectList VehicleMakes { get; set; }
    public string VehicleModelMake { get; set; }
    public string SearchString { get; set; }
}