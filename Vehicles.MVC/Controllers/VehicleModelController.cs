using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehicles.MVC.Data;
using Vehicles.MVC.Models;
using Vehicles.MVC.Service;
using Vehicles.MVC.Service.VehicleModelRepository;

namespace Vehicles.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleMakeRepository _vehicleMakeRepository;
        private readonly IVehicleModelRepository _vehicleModelRepository;


        public VehicleModelController(IVehicleModelRepository vehicleModelRepository, IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleMakeRepository = vehicleMakeRepository;
            _vehicleModelRepository = vehicleModelRepository;
        }

        // GET: VehicleModel
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string vehicleModelMake, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewData["MakeSortParm"] = sortOrder == "make" ? "make_desc" : "make";

            if (vehicleModelMake != null)
            {
                pageNumber = 1;
            }
            else
            {
                vehicleModelMake = currentFilter;
            }

            ViewData["CurrentFilter"] = vehicleModelMake; 
            
            // section for filter by make
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();

            var vehicleModels = await _vehicleModelRepository.GetVehicleModelsAsync();

            if (!string.IsNullOrEmpty(vehicleModelMake))
            {
                vehicleModels = vehicleModels.Where(v => v.VehicleMake.Name == vehicleModelMake);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Name);
                    break;
                case "abrv":
                    vehicleModels = vehicleModels.OrderBy(v => v.Abrv);
                    break;
                case "abrv_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Abrv);
                    break;
                case "make":
                    vehicleModels = vehicleModels.OrderBy(v => v.VehicleMake.Name);
                    break;
                case "make_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.VehicleMake.Name);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(v => v.Name);
                    break;
            }

            IQueryable<VehicleModel> source = vehicleModels;
            var vehicleModelsVm = _mapper.ProjectTo<VehicleModelViewModel>(source);
            int pageSize = 3;
            var paginatedList = await PaginatedList<VehicleModelViewModel>.CreateAsync(vehicleModelsVm, pageNumber ?? 1, pageSize);
            

            var vehicleModelVm = new VehicleModelIndexViewModel
            {
                VehicleMakes = new SelectList(vehicleMakeQuery.Select(vM => vM.Name)),
                VehicleModels = paginatedList,
                VehicleModelMake = vehicleModelMake,
                SearchString = currentFilter
            };
            
            return View(vehicleModelVm);
        }

        // GET: VehicleModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleModelRepository.GetVehicleModelAsync(id);
            
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModel/Create
        public async Task<IActionResult> Create()
        {
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();

            ViewData["VehicleMakeId"] = new SelectList(vehicleMakeQuery, "Id", "Name");
            return View();
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await _vehicleModelRepository.AddVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();
            ViewData["VehicleMakeId"] = new SelectList(vehicleMakeQuery, "Id", "Name", vehicleModel.VehicleMakeId);
            
            return View(vehicleModel);
        }

        // GET: VehicleModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleModelRepository.GetVehicleModelAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();

            ViewData["VehicleMakeId"] = new SelectList(vehicleMakeQuery, "Id", "Name", vehicleModel.VehicleMakeId);
            return View(vehicleModel);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (!await _vehicleModelRepository.VehicleModelExistsAsync(vehicleModel.Id))
                {
                    return NotFound();
                }

                await _vehicleModelRepository.UpdateVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();
            ViewData["VehicleMakeId"] = new SelectList(vehicleMakeQuery, "Id", "Id", vehicleModel.VehicleMakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleModelRepository.GetVehicleModelAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleModelRepository.DeleteVehicleModelAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
