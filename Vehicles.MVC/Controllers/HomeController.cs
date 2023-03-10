using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.MVC.ViewModels;
using Vehicles.Service.Data;
using Vehicles.Service.Models;

namespace Vehicles.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly VehicleContext _context;

    public HomeController(ILogger<HomeController> logger, VehicleContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {

        int totalVehicleMakes = _context.VehicleMake.Count();
        int totalVehicleModels = _context.VehicleModel.Count();

        var totalVm = new SumViewModel
        {
            VehicleMakesSum = totalVehicleMakes,
            VehicleModelsSum = totalVehicleModels
        };
        return View(totalVm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
