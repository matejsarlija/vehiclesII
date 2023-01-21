using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicles.MVC.Helpers;
using Vehicles.MVC.ViewModels;
using Vehicles.Service.Helpers;
using Vehicles.Service.Models;
using Vehicles.Service.Service.VehicleMakeRepository;

namespace Vehicles.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;
        private readonly IMapper _mapper;


        public VehicleMakeController(IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
            _mapper = mapper;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(VehicleMakeQuery vehicleMakeQuery)
        {
            ViewData["CurrentSort"] = vehicleMakeQuery.SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(vehicleMakeQuery.SortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = vehicleMakeQuery.SortOrder == "abrv" ? "abrv_desc" : "abrv";

            if (vehicleMakeQuery.SearchString != null)
            {
                vehicleMakeQuery.PageNumber = 1;
            }
            else
            {
                vehicleMakeQuery.SearchString = vehicleMakeQuery.CurrentFilter;
            }

            ViewData["CurrentFilter"] = vehicleMakeQuery.SearchString;

            var vehicleMakes =
                await _vehicleMakeRepository.GetVehicleMakesAsync(vehicleMakeQuery.SortOrder, vehicleMakeQuery.CurrentFilter, vehicleMakeQuery.SearchString, vehicleMakeQuery.PageNumber);

            var vehicleMakesVm = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);

            var paginatedVehicleMakesVm = new PaginatedList<VehicleMakeViewModel>(vehicleMakesVm, vehicleMakes.TotalCount, vehicleMakeQuery.PageNumber ?? 1, 3);

            return View(paginatedVehicleMakesVm);
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMakeRepository.GetVehicleMakeByIdAsync(id.Value);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeVm);
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMakeViewModel vehicleMakeVm)
        {
            if (ModelState.IsValid)
            {
                var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeVm);
                await _vehicleMakeRepository.CreateVehicleMakeAsync(vehicleMake);
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleMakeVm);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMakeRepository.GetVehicleMakeByIdAsync(id.Value);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeVm);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMakeViewModel vehicleMakeVm)
        {
            if (id != vehicleMakeVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeVm);

                if (! await _vehicleMakeRepository.VehicleMakeExistsAsync(vehicleMake.Id))
                {
                    return NotFound();
                }

                await _vehicleMakeRepository.UpdateVehicleMakeAsync(vehicleMake);
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleMakeVm);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var vehicleMake = await _vehicleMakeRepository.GetVehicleMakeByIdAsync(id.Value);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeVm);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleMakeRepository.DeleteVehicleMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
