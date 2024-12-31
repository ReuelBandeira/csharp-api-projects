using Api.Domain.UseCases.FilesMinios.Dtos;
using Api.Domain.UseCases.FilesMinios.Models;
using Api.Shared.Helpers;

namespace Api.Domain.UseCases.FilesMinios.Services.Interfaces;

public interface IFileMinioService
{
    /// <summary>
    /// Obtém uma lista paginada de FileMinios com base nos parâmetros de paginação e filtro.
    /// </summary>
    Task<PaginatedList<FileMinio>> GetAllAsync(PaginationParams paginationParams, FilterParams filterParams);

    /// <summary>
    /// Obtém um FileMinio pelo ID.
    /// </summary>
    Task<FileMinio?> GetByIdAsync(int id);

    /// <summary>
    /// Adiciona um novo FileMinio.
    /// </summary>
    Task<FileMinio> AddAsync(CreateFileMinioDto dto);

    /// <summary>
    /// Atualiza um FileMinio existente.
    /// </summary>
    Task<FileMinio?> UpdateAsync(int id, UpdateFileMinioDto dto);

    /// <summary>
    /// Realiza exclusão lógica de um FileMinio.
    /// </summary>
    Task<bool> SoftDeleteAsync(int id);

    /// <summary>
    /// Faz o upload de um arquivo para o bucket MinIO e cria a entrada no banco de dados.
    /// </summary>
    Task<string> UploadFileAsync(UploadFileDto dto);
}
