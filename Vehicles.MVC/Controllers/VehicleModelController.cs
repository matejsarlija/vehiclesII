using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicles.MVC.Helpers;
using Vehicles.MVC.ViewModels;
using Vehicles.Service.Helpers;
using Vehicles.Service.Models;
using Vehicles.Service.Service.VehicleMakeRepository;
using Vehicles.Service.Service.VehicleModelRepository;

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
        public async Task<IActionResult> Index(VehicleModelQuery vehicleModelQuery)
        {
            ViewData["CurrentSort"] = vehicleModelQuery.SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(vehicleModelQuery.SortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = vehicleModelQuery.SortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewData["MakeSortParm"] = vehicleModelQuery.SortOrder == "make" ? "make_desc" : "make";

            if (!string.IsNullOrEmpty(vehicleModelQuery.VehicleModelMake))
            {
                vehicleModelQuery.PageNumber = 1;
            }
            else
            {
                vehicleModelQuery.VehicleModelMake = vehicleModelQuery.CurrentFilter;
            }

            ViewData["CurrentFilter"] = vehicleModelQuery.VehicleModelMake; 
            
            // section for filter by make
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();

            var vehicleModels = await _vehicleModelRepository.GetVehicleModelsAsync();

            if (!string.IsNullOrEmpty(vehicleModelQuery.VehicleModelMake))
            {
                vehicleModels = vehicleModels.Where(v => v.VehicleMake.Name == vehicleModelQuery.VehicleModelMake);
            }

            switch (vehicleModelQuery.SortOrder)
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
            var paginatedList = await PaginatedList<VehicleModelViewModel>.CreateAsync(vehicleModelsVm, vehicleModelQuery.PageNumber ?? 1, pageSize);
            

            var vehicleModelVm = new VehicleModelIndexViewModel
            {
                VehicleMakes = new SelectList(vehicleMakeQuery.Select(vM => vM.Name)),
                VehicleModels = paginatedList,
                VehicleModelMake = vehicleModelQuery.VehicleModelMake,
                SearchString = vehicleModelQuery.CurrentFilter
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

            var vehicleModelVm = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(vehicleModelVm);
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
        public async Task<IActionResult> Create([Bind("Id,VehicleMakeId,Name,Abrv")] VehicleModelViewModel vehicleModelVm)
        {
            if (ModelState.IsValid)
            {
                var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelVm);
                await _vehicleModelRepository.AddVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();
            ViewData["VehicleMakeId"] = new SelectList(vehicleMakeQuery, "Id", "Name");

            return View(vehicleModelVm);
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
            var vehicleModelVm = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelVm);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleMakeId,Name,Abrv")] VehicleModelViewModel vehicleModelVm)
        {
            if (id != vehicleModelVm.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelVm);

                await _vehicleModelRepository.UpdateVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();
            ViewData["VehicleMakeId"] = new SelectList(vehicleMakeQuery, "Id", "Id", vehicleModelVm.VehicleMakeId);
            
            return View(vehicleModelVm);
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

            var vehicleModelVm = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelVm);
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
