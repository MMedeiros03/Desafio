using DesafioBenner.DTO;
using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
namespace DesafioBenner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingController : ControllerBase
{
    private readonly IParkingService _service;

    public ParkingController(IParkingService serviceVehicle)
    {
        _service = serviceVehicle;
    }

    [HttpGet]
    public async Task<ActionResult<List<Parking>>> GetAsync(long id = 0)
    {
        if(id == 0)
        {
            return Ok(await _service.GetAllAsync());
        }
        else
        {
            return Ok(await _service.GetByIdAsync(id));
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Parking entity)
    {
        return Ok(await _service.PostAsync(entity));
    }

    [HttpPut]
    public async Task<ActionResult> PutAsync(ParkingDepartureDTO entity)
    {
        return Ok(await _service.PutAsync(entity));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
}



