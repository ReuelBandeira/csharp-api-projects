using Minio;
using Minio.DataModel;
using Microsoft.Extensions.Options;
using System.IO;

public class MinioService : IMinioService
{
    private readonly MinioClient _minioClient;
    private readonly MinioSettings _minioSettings;

    public MinioService(IOptions<MinioSettings> minioSettings)
    {
        _minioSettings = minioSettings.Value;

        // Inicializa o MinioClient
        _minioClient = new MinioClient(
            _minioSettings.Endpoint,
            _minioSettings.AccessKey,
            _minioSettings.SecretKey
        );

        // Se o SSL for necessário, você pode configurá-lo com base no seu endpoint
        if (_minioSettings.UseSSL)
        {
            _minioClient = _minioClient.WithSSL(); // Adiciona suporte a SSL, caso seja verdadeiro
        }
    }

    public async Task UploadFileAsync(IFormFile file, string bucketName, string objectName)
    {
        // Verifica se o bucket existe, se não, cria o bucket
        bool found = await _minioClient.BucketExistsAsync(bucketName);
        if (!found)
        {
            await _minioClient.MakeBucketAsync(bucketName);
        }

        // Envia o arquivo para o MinIO
        using (var stream = file.OpenReadStream())
        {
            await _minioClient.PutObjectAsync(bucketName, objectName, stream, file.Length, file.ContentType);
        }
    }
}
