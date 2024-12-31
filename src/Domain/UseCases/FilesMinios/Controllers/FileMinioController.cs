using Api.Domain.UseCases.FilesMinios.Dtos;
using Api.Domain.UseCases.FilesMinios.Services.Interfaces;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.FilesMinios.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FileMinioController : ControllerBase
{
    private readonly IFileMinioService _service;


    // Constructor with dependency injection
    public FileMinioController(IFileMinioService service)
    {
        _service = service;
    }

    [HttpGet]
    [ServiceFilter(typeof(PaginationHeaderFilter))]
    public async Task<IActionResult> GetAll([FromServices] PaginationParams paginationParams, [FromQuery] FilterParams filterParams)
    {
        var fileMinio = await _service.GetAllAsync(paginationParams, filterParams);
        fileMinio.AddPaginationHeaders(Response);
        return Ok(fileMinio);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var fileMinio = await _service.GetByIdAsync(id);
        return fileMinio == null ? NotFound(new { message = "FileMinio not exists." }) : Ok(fileMinio);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFileMinioDto dto)
    {
        try
        {
            var fileMinio = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = fileMinio.FilMinId }, fileMinio);
        }
        catch (EntityNotCreatedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateFileMinioDto dto)
    {
        try
        {
            var fileMinio = await _service.UpdateAsync(id, dto);
            return fileMinio == null ? NotFound(new { message = "FileMinio not exists." }) : Ok(fileMinio);
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
        return await _service.SoftDeleteAsync(id) ? NoContent() : NotFound(new { message = "FileMinio not exists." });
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file uploaded." });
        }

        if (file.Length == 0)
        {
            return BadRequest(new { message = "Uploaded file is empty." });
        }

        if (file.ContentType is not ("image/png" or "image/jpeg" or "application/pdf"))
        {
            return BadRequest(new { message = "Only png, jpg, or pdf files are allowed." });
        }

        try
        {
            using var stream = file.OpenReadStream();
            var uploadResult = await _service.UploadFileAsync(new UploadFileDto
            {
                Stream = stream,
                FileName = file.FileName,
                FileType = file.ContentType,
                FileLength = file.Length
            });

            return Ok(new { message = "File uploaded successfully.", result = uploadResult });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "An unexpected error occurred during file upload.",
                details = ex.Message
            });
        }
    }
}
