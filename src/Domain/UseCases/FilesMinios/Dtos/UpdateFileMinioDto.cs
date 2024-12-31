namespace Api.Domain.UseCases.FilesMinios.Dtos;

public class UpdateFileMinioDto
{
    public int FilMinId { get; set; }
    public required string BucketName { get; set; }
    public required string FileName { get; set; }
    public required string FileType { get; set; }
    public required ulong FileSize { get; set; }
}