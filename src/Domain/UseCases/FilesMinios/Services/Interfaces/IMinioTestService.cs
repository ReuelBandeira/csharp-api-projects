namespace Api.Domain.UseCases.FilesMinios.Services.Interfaces
{
    public interface IMinioTestService
    {
        // Método para testar a conexão com o MinIO
        Task<bool> TestConnectionAsync();
    }
}
