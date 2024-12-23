using Api.Domain.UseCases.TypesChecklists.Dtos;
using Api.Domain.UseCases.TypesChecklists.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.TypesChecklists.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TypesChecklistController(ITypesChecklistService service) : ControllerBase
{
    private readonly ITypesChecklistService _service = service;

    [HttpGet]
    [ServiceFilter(typeof(PaginationHeaderFilter))]
    public async Task<IActionResult> getAll([FromServices] PaginationParams paginationParams, [FromQuery] FilterParams filterParams)
    {
        var typesChecklist = await _service.getAllAsync(paginationParams, filterParams);
        typesChecklist.AddPaginationHeaders(Response);
        return Ok(typesChecklist);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById(int id)
    {
        var typesChecklist = await _service.getByIdAsync(id);
        return typesChecklist == null ? NotFound(new { message = "TypesChecklist not exists." }) : Ok(typesChecklist);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTypesChecklistDto dto)
    {
        try
        {
            var typesChecklist = await _service.addAsync(dto);

            return CreatedAtAction(nameof(getById), new { id = typesChecklist.chTypId }, typesChecklist);
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
    public async Task<IActionResult> Update(int id, UpdateTypesChecklistDto dto)
    {
        try
        {
            var typesChecklist = await _service.updateAsync(id, dto);
            return typesChecklist == null ? NotFound(new { message = "TypesChecklist not exists." }) : Ok(typesChecklist);
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
        return await _service.softDeleteAsync(id) ? NoContent() : NotFound(new { message = "TypesChecklist not exists." });
    }
}