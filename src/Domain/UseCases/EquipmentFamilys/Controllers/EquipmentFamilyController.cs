using Api.Domain.UseCases.EquipmentFamilys.Dtos;
using Api.Domain.UseCases.EquipmentFamilys.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.EquipmentFamilys.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EquipmentFamilyController(IEquipmentFamilyService service) : ControllerBase
{
    private readonly IEquipmentFamilyService _service = service;

    [HttpGet]
    [ServiceFilter(typeof(PaginationHeaderFilter))]
    public async Task<IActionResult> getAll([FromServices] PaginationParams paginationParams, [FromQuery] FilterParams filterParams)
    {
        var equipmentFamily = await _service.getAllAsync(paginationParams, filterParams);
        equipmentFamily.AddPaginationHeaders(Response);
        return Ok(equipmentFamily);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById(int id)
    {
        var equipmentFamily = await _service.getByIdAsync(id);
        return equipmentFamily == null ? NotFound(new { message = "Equipment family not exists." }) : Ok(equipmentFamily);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEquipmentFamilyDto dto)
    {
        try
        {
            var equipmentFamily = await _service.addAsync(dto);

            return CreatedAtAction(nameof(getById), new { id = equipmentFamily.eqFamId }, equipmentFamily);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEquipmentFamilyDto dto)
    {
        try
        {
            var equipmentFamily = await _service.updateAsync(id, dto);
            return equipmentFamily == null ? NotFound(new { message = "Equipment family not exists." }) : Ok(equipmentFamily);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _service.softDeleteAsync(id) ? NoContent() : NotFound(new { message = "Equipment family not exists." });
    }
}