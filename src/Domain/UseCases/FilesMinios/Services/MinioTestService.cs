using Api.Domain.UseCases.FilesMinios.Services.Interfaces;
using Minio;

namespace Api.Domain.UseCases.FilesMinios.Services
{
    public class MinioTestService : IMinioTestService
    {
        private readonly MinioClient _minioClient;

        public MinioTestService(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task<IActionResult> TestConnectionAsync()
        {
            try
            {
                // Teste de conexão
                var result = await _minioClient.ListBucketsAsync();

                // Verifique se a conexão foi bem-sucedida
                if (result?.Buckets == null || !result.Buckets.Any())
                {
                    return BadRequest(new { Message = "No buckets found" });
                }

                return Ok(new { Message = "MinIO connection successful!", Buckets = result.Buckets.Select(b => b.Name) });
            }
            catch (Exception ex)
            {
                // Logando o erro detalhado
                return BadRequest(new { Message = "Error connecting to MinIO", Details = ex.Message });
            }
        }

        private IActionResult Ok(object value)
        {
            throw new NotImplementedException();
        }

        private IActionResult BadRequest(object value)
        {
            throw new NotImplementedException();
        }

        Task<bool> IMinioTestService.TestConnectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
