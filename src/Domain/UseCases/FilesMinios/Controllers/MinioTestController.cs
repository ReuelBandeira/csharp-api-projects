
using Minio;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinioTestController : ControllerBase
    {
        private readonly MinioClient _minioClient;

        public MinioTestController(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnectionAsync()
        {
            try
            {
                // Teste a conexão
                var result = await _minioClient.ListBucketsAsync();

                // Verifique se a resposta não é nula
                if (result?.Buckets == null || !result.Buckets.Any())
                {
                    return BadRequest(new { Message = "No buckets found" });
                }

                // Retorna o sucesso, com os nomes dos buckets
                return Ok(new { Message = "MinIO connection successful!", Buckets = result.Buckets.Select(b => b.Name) });
            }
            catch (Exception ex)
            {
                // Logando o erro detalhado
                return BadRequest(new { Message = "Error connecting to MinIO", Details = ex.Message });
            }
        }
    }


}
