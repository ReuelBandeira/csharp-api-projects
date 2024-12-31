using Api.Shared.Bases;

namespace Api.Domain.UseCases.FilesMinios.Models;

[Table("file_minio")]
public class FileMinio : AuditedBase
{
    [Key]
    public int FilMinId { get; set; }

    [Required]
    [MaxLength(120)]
    public required string BucketName { get; set; }

    [Required]
    [MaxLength(120)]
    public required string FileName { get; set; }

    [Required]
    [MaxLength(120)]
    public required string FileType { get; set; }
    public required ulong FileSize { get; set; }
}