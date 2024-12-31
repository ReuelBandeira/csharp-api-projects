using Api.Domain.UseCases.FilesMinios.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.FilesMinios.Repositories.Interfaces;

public interface IFileMinioRepository
{
    IQueryable<FileMinio> GetAll();
    Task<FileMinio?> GetByIdAsync(int id);
    Task AddAsync(FileMinio fileMinio);
    Task UpdateAsync(FileMinio fileMinio);
    Task SoftDeleteAsync(FileMinio fileMinio);
    Task<bool> ValidateCreateAsync(string name);
    Task<bool> ValidateUpdateAsync(string name, int id);

    // Adicionados para operações de arquivos no MinIO
    Task<FileMinio?> GetFileByFileNameAsync(string fileName); // Obter arquivo pelo nome
    Task<bool> BucketExistsAsync(string bucketName);         // Verificar existência do bucket
    Task UploadFileAsync(string bucketName, string fileName, string fileType, ulong fileSize, Stream stream); // Upload de arquivo
    Task CreateBucketAsync(string bucketName);               // Criar bucket

}
