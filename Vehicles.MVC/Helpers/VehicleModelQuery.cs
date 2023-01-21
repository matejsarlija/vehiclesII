namespace Vehicles.MVC.Helpers;

public class VehicleModelQuery
{
    public string SortOrder { get; set; }
    public string CurrentFilter { get; set; }
    public string VehicleModelMake { get; set; }
    public int? PageNumber { get; set; }
}