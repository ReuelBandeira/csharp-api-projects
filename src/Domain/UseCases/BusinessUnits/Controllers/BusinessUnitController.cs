using Api.Domain.UseCases.BusinessUnits.Dtos;
using Api.Domain.UseCases.BusinessUnits.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.BusinessUnits.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BusinessUnitController(IBusinessUnitService service) : ControllerBase
{
    private readonly IBusinessUnitService _service = service;

    [HttpGet]
    [ServiceFilter(typeof(PaginationHeaderFilter))]
    public async Task<IActionResult> getAll([FromServices] PaginationParams paginationParams, [FromQuery] FilterParams filterParams)
    {
        var businessUnit = await _service.getAllAsync(paginationParams, filterParams);
        businessUnit.AddPaginationHeaders(Response);
        return Ok(businessUnit);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById(int id)
    {
        var businessUnit = await _service.getByIdAsync(id);
        return businessUnit == null ? NotFound() : Ok(businessUnit);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBusinessUnitDto dto)
    {
        var businessUnit = await _service.addAsync(dto);
        return CreatedAtAction(nameof(getById), new { id = businessUnit.busId }, businessUnit);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBusinessUnitDto dto)
    {
        var businessUnit = await _service.updateAsync(id, dto);
        return businessUnit == null ? NotFound() : Ok(businessUnit);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _service.softDeleteAsync(id) ? NoContent() : NotFound();
    }
}