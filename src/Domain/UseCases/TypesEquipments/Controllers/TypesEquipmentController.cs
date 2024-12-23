using Api.Domain.UseCases.TypesEquipments.Dtos;
using Api.Domain.UseCases.TypesEquipments.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesEquipments.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TypesEquipmentController(ITypesEquipmentService service) : ControllerBase
{
    private readonly ITypesEquipmentService _service = service;

    [HttpGet]
    [ServiceFilter(typeof(PaginationHeaderFilter))]
    public async Task<IActionResult> getAll([FromServices] PaginationParams paginationParams, [FromQuery] FilterParams filterParams)
    {
        var typesEquipment = await _service.getAllAsync(paginationParams, filterParams);
        typesEquipment.AddPaginationHeaders(Response);
        return Ok(typesEquipment);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById(int id)
    {
        var typesEquipment = await _service.getByIdAsync(id);
        return typesEquipment == null ? NotFound(new { message = "TypesEquipment not exists." }) : Ok(typesEquipment);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTypesEquipmentDto dto)
    {
        try
        {
            var typesEquipment = await _service.addAsync(dto);

            return CreatedAtAction(nameof(getById), new { id = typesEquipment.eqTypId }, typesEquipment);
        }
        catch (EntityNotCreatedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTypesEquipmentDto dto)
    {
        try
        {
            var typesEquipment = await _service.updateAsync(id, dto);
            return typesEquipment == null ? NotFound(new { message = "TypesEquipment not exists." }) : Ok(typesEquipment);
        }
        catch (EntityNotUpdatedException ex)
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
        return await _service.softDeleteAsync(id) ? NoContent() : NotFound(new { message = "TypesEquipment not exists." });
    }
}