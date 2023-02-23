using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Model.DTO;
using Vehicles.Repository;
using Vehicles.Service.Helpers;
using Vehicles.Service.Models;

namespace Vehicles.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VehicleMakeApiController : ControllerBase
{
    private readonly IVehicleUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public VehicleMakeApiController(IVehicleUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/VehicleMakeApi
    [HttpGet]
    public async Task<ActionResult<PaginatedList<VehicleMakeDto>>> GetVehicleMakes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var vehicleMakes = _unitOfWork.VehicleMakeRepository.Get();
        //var paginatedList = await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes.Result, pageNumber, pageSize);

        var mappedList = _mapper.Map<IQueryable<VehicleMakeDto>>(vehicleMakes);
        return Ok(await PaginatedList<VehicleMakeDto>.CreateAsync(mappedList, pageNumber, pageSize));
    }

    // GET: api/VehicleMakeApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleMakeDto>> GetVehicleMake(int id)
    {
        var vehicleMake = await _unitOfWork.VehicleMakeRepository.GetByIdAsync(id);

        if (vehicleMake == null)
        {
            return NotFound();
        }

        var mapped = _mapper.Map<VehicleMakeDto>(vehicleMake);

        return mapped;
    }

    // PUT: api/VehicleMakeApi/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehicleMake(int id, VehicleMakeDto vehicleMakeDto)
    {
        if (id != vehicleMakeDto.Id)
        {
            return BadRequest();
        }

        var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);

        _unitOfWork.VehicleMakeRepository.Update(vehicleMake);

        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VehicleMakeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/VehicleMakeApi
    [HttpPost]
    public async Task<ActionResult<VehicleMakeDto>> PostVehicleMake(VehicleMake vehicleMakeDto)
    {
        var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);

        _unitOfWork.VehicleMakeRepository.InsertAsync(vehicleMake);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetVehicleMake", new { id = vehicleMakeDto.Id }, vehicleMakeDto);
    }

    // DELETE: api/VehicleMakeApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicleMake(int id)
    {
        var vehicleMake = await _unitOfWork.VehicleMakeRepository.GetByIdAsync(id);
        if (vehicleMake == null)
        {
            return NotFound();
        }

        _unitOfWork.VehicleMakeRepository.Delete(vehicleMake);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

    private bool VehicleMakeExists(int id)
    {
        return _unitOfWork.VehicleMakeRepository.GetByIdAsync(id) != null;
    }
}

