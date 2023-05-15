using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
namespace DesafioBenner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly IPriceService _service;

    public PriceController(IPriceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Price>>> GetAsync(long id = 0)
    {
        if (id == 0)
        {
            return Ok(await _service.GetAllAsync());
        }
        else
        {
            return Ok(await _service.GetByIdAsync(id));
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Price entity)
    {
        return Ok(await _service.PostAsync(entity));
    }

    [HttpPut]
    public async Task<ActionResult> PutAsync(Price entity)
    {
        return Ok(await _service.PutAsync(entity));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
}



