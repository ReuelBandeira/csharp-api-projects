namespace Api.Domain.UseCases.FilesMinios.Dtos;

public class FileMinioDto
{
    public int FilMinId { get; set; }
    [Required]
    public required string BucketName { get; set; }
    public required string FileName { get; set; }
    public required string FileType { get; set; }
    public required ulong FileSize { get; set; }
    public bool IsDeleted { get; set; }
}