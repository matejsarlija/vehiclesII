namespace Vehicles.MVC.Helpers;

public abstract class IndexQueryParameters
{
    public string SortOrder { get; set; }
    public string CurrentFilter { get; set; }
    public int? PageNumber { get; set; }
    
}