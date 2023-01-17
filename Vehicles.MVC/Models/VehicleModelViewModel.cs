namespace Vehicles.MVC.Models;

public class VehicleModelViewModel
{
        public int Id { get; set; }
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public VehicleMake VehicleMake { get; set; }

    
}