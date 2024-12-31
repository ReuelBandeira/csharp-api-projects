using Minio;
using Api.Domain.UseCases.FilesMinios.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Api.Domain.UseCases.FilesMinios.Services
{
    public class MinioTestService : IMinioTestService
    {
        private readonly MinioClient _minioClient;

        public MinioTestService(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        // Implementação do método para testar a conexão com o Minio
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                // Tentando listar os buckets como uma forma de testar a conexão
                var result = await _minioClient.ListBucketsAsync();

                // Verifica se a lista de buckets não está vazia
                return result?.Buckets != null && result.Buckets.Count > 0;
            }
            catch (Exception ex)
            {
                // Caso ocorra um erro, retorna false
                Console.WriteLine($"Erro na conexão com MinIO: {ex.Message}");
                return false;
            }
        }
    }
}
