public interface IMinioService
{
    Task UploadFileAsync(IFormFile file, string bucketName, string objectName);
}
