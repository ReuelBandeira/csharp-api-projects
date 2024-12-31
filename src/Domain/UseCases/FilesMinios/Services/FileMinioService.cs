using Api.Domain.UseCases.FilesMinios.Dtos;
using Api.Domain.UseCases.FilesMinios.Models;
using Api.Domain.UseCases.FilesMinios.Repositories.Interfaces;
using Api.Domain.UseCases.FilesMinios.Services.Interfaces;
using Api.Shared.Helpers;
using Minio;
using System.IO;

namespace Api.Domain.UseCases.FilesMinios.Services;

public class FileMinioService : IFileMinioService
{
    private readonly IFileMinioRepository _repository;

    public FileMinioService(IFileMinioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedList<FileMinio>> GetAllAsync(PaginationParams paginationParams, FilterParams filterParams)
    {
        var query = _repository.GetAll().Where(x => x.deletedAt == null);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(filterParams.name))
        {
            query = query.Where(x => x.BucketName.Contains(filterParams.name));
        }

        if (filterParams.createdAfter.HasValue)
        {
            query = query.Where(x => x.createdAt >= filterParams.createdAfter.Value);
        }

        if (filterParams.createdBefore.HasValue)
        {
            query = query.Where(x => x.createdAt <= filterParams.createdBefore.Value);
        }

        if (!string.IsNullOrEmpty(filterParams.status))
        {
            query = query.Where(x => x.FileName == filterParams.status);
        }

        // Retornar resultados paginados
        return await PaginatedList<FileMinio>.CreateAsync(query, paginationParams.pageNumber, paginationParams.pageSize);
    }

    public async Task<FileMinio?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<FileMinio> AddAsync(CreateFileMinioDto dto)
    {
        if (await _repository.ValidateCreateAsync(dto.BucketName))
        {
            throw new EntityNotCreatedException("FileMinio name already exists.");
        }

        var fileMinio = new FileMinio
        {
            BucketName = dto.BucketName,
            FileName = dto.FileName,
            FileType = dto.FileType,
            FileSize = dto.FileSize
        };

        await _repository.AddAsync(fileMinio);
        return fileMinio;
    }

    public async Task<FileMinio?> UpdateAsync(int id, UpdateFileMinioDto dto)
    {
        if (await _repository.ValidateUpdateAsync(dto.BucketName, id))
        {
            throw new EntityNotUpdatedException("FileMinio name already exists.");
        }

        var fileMinio = await _repository.GetByIdAsync(id);
        if (fileMinio == null) return null;

        fileMinio.BucketName = dto.BucketName;
        fileMinio.FileName = dto.FileName;
        fileMinio.FileType = dto.FileType;
        fileMinio.FileSize = dto.FileSize;

        await _repository.UpdateAsync(fileMinio);
        return fileMinio;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var fileMinio = await _repository.GetByIdAsync(id);
        if (fileMinio == null) return false;

        await _repository.SoftDeleteAsync(fileMinio);
        return true;
    }
    public async Task<string> UploadFileAsync(UploadFileDto dto)
    {
        const string bucketName = "example-bucket";

        var fileName = dto.FileName.ToLower();
        var existingFile = await _repository.GetFileByFileNameAsync(fileName);
        if (existingFile is not null)
        {
            var ext = Path.GetExtension(existingFile.FileName);
            var baseName = Path.GetFileNameWithoutExtension(existingFile.FileName);
            var copy = 1;

            do
            {
                fileName = $"{baseName}({copy++}){ext}";
            }
            while (await _repository.GetFileByFileNameAsync(fileName) is not null);
        }

        try
        {
            // Log para garantir que estamos chegando até aqui
            Console.WriteLine($"Preparing to upload file {fileName}");

            // Calcular o tamanho do arquivo a partir do Stream
            var fileSize = (ulong)dto.Stream.Length;

            // Faz upload do arquivo
            await _repository.UploadFileAsync(bucketName, fileName, dto.FileType, fileSize, dto.Stream);

            Console.WriteLine($"File {fileName} uploaded successfully.");

            // Cria o registro no banco de dados
            var fileMinio = new FileMinio
            {
                BucketName = bucketName,
                FileName = fileName,
                FileType = dto.FileType,
                FileSize = fileSize
            };

            await _repository.AddAsync(fileMinio);

            return fileName;
        }
        catch (Exception ex)
        {
            // Log detalhado para capturar exceção
            Console.WriteLine($"Error uploading file: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            throw; // Re-throw the exception to handle it upstream
        }
    }











}
