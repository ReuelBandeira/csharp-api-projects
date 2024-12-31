namespace Api.Domain.UseCases.FilesMinios.Dtos;

public class CreateFileMinioDto
{
    public int FilMinId { get; set; }
    public required string BucketName { get; set; } = string.Empty;
    public required string FileName { get; set; } = string.Empty;
    public required string FileType { get; set; }
    public required ulong FileSize { get; set; }
}

