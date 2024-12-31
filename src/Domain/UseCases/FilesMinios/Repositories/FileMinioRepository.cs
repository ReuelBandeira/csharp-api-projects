using Api.Domain.UseCases.FilesMinios.Repositories.Interfaces;
using Api.Domain.UseCases.FilesMinios.Models;
using Api.Infra.Database;
using Minio; // Biblioteca MinIO
using Minio.DataModel; // Para tipos relacionados ao MinIO
using Microsoft.EntityFrameworkCore;
using Minio.DataModel.Args; // Para consultas assíncronas no EF

namespace Api.Domain.UseCases.FilesMinios.Repositories;

public class FileMinioRepository : IFileMinioRepository
{
    private readonly AppDbContext _context;
    private readonly MinioClient _minioClient;

    public FileMinioRepository(AppDbContext dbContext, MinioClient minioClient)
    {
        _context = dbContext;
        _minioClient = minioClient;
    }

    public IQueryable<FileMinio> GetAll()
    {
        return _context.FileMinios.AsQueryable();
    }

    public async Task<FileMinio?> GetByIdAsync(int id)
    {
        return await _context.FileMinios.FirstOrDefaultAsync(x => x.FilMinId == id && x.deletedAt == null);
    }

    public async Task AddAsync(FileMinio fileMinio)
    {
        fileMinio.createdAt = DateTime.UtcNow;
        await _context.FileMinios.AddAsync(fileMinio);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FileMinio fileMinio)
    {
        fileMinio.updatedAt = DateTime.UtcNow;
        _context.FileMinios.Update(fileMinio);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(FileMinio fileMinio)
    {
        fileMinio.deletedAt = DateTime.UtcNow;
        await UpdateAsync(fileMinio);
    }

    public async Task<bool> ValidateCreateAsync(string name)
    {
        return await _context.FileMinios
            .AnyAsync(x => x.BucketName == name && x.deletedAt == null);
    }

    public async Task<bool> ValidateUpdateAsync(string name, int id)
    {
        return await _context.FileMinios
            .AnyAsync(x => x.BucketName == name && x.FilMinId != id && x.deletedAt == null);
    }

    // Métodos do MinIO
    public async Task<FileMinio?> GetFileByFileNameAsync(string fileName)
    {
        return await _context.FileMinios
            .FirstOrDefaultAsync(f => f.FileName == fileName && f.deletedAt == null);
    }

    public async Task<bool> BucketExistsAsync(string bucketName)
    {
        try
        {
            return await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        }
        catch (Exception ex)
        {
            throw new Exception("Error checking bucket existence.", ex);
        }
    }

    public async Task UploadFileAsync(string bucketName, string fileName, string fileType, ulong fileSize, Stream stream)
    {
        try
        {
            // Log de depuração
            Console.WriteLine($"Verificando existência do bucket: {bucketName}");

            if (!await BucketExistsAsync(bucketName))
            {
                Console.WriteLine($"Bucket {bucketName} não existe. Criando...");
                await CreateBucketAsync(bucketName);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithContentType(fileType)
                .WithStreamData(stream)
                .WithObjectSize((long)fileSize);

            Console.WriteLine($"Iniciando upload do arquivo: {fileName}");
            await _minioClient.PutObjectAsync(putObjectArgs);
            Console.WriteLine($"Upload do arquivo {fileName} concluído com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro durante o upload do arquivo {fileName}: {ex.Message}");
            throw new Exception($"Error uploading file '{fileName}' to bucket '{bucketName}': {ex.Message}", ex);
        }
    }



    // public async Task UploadFileAsync(string bucketName, string fileName, string fileType, ulong fileSize, Stream stream)
    // {
    //     try
    //     {
    //         var putObjectArgs = new PutObjectArgs()
    //             .WithBucket(bucketName)
    //             .WithObject(fileName)
    //             .WithContentType(fileType)
    //             .WithStreamData(stream)
    //             .WithObjectSize((long)fileSize);

    //         await _minioClient.PutObjectAsync(putObjectArgs);
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception("Error uploading file.", ex);
    //     }
    // }

    public async Task CreateBucketAsync(string bucketName)
    {
        try
        {
            if (!await BucketExistsAsync(bucketName))
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating bucket.", ex);
        }
    }
}
