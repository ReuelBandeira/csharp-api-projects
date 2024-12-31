using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IMinioService _minioService;

    public FileUploadController(IMinioService minioService)
    {
        _minioService = minioService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo enviado.");
        }

        try
        {
            string bucketName = "example-bucket";  // Nome do seu bucket
            string objectName = file.FileName;     // Nome do arquivo que será armazenado no MinIO
            await _minioService.UploadFileAsync(file, bucketName, objectName);
            return Ok(new { message = "Arquivo enviado com sucesso!", fileName = objectName });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao enviar o arquivo: {ex.Message}");
        }
    }
}
