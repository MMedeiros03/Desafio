using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
namespace DesafioBenner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _service;

    public VehicleController(IVehicleService serviceVehicle)
    {
        _service = serviceVehicle;
    }

    [HttpGet]
    public async Task<ActionResult<List<Vehicle>>> GetAsync(string licensePlate = "")
    {
        if (licensePlate == "")
        {
            return Ok(await _service.GetAllAsync());
        }
        else
        {
            return Ok(await _service.GetByIdAsync(licensePlate));
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Vehicle entity)
    {
        return Ok(await _service.PostAsync(entity));
    }

    [HttpPut]
    public async Task<ActionResult> PutAsync(Vehicle entity)
    {
        return Ok(await _service.PutAsync(entity));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
}



