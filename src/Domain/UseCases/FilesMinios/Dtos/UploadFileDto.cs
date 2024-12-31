namespace Api.Domain.UseCases.FilesMinios.Dtos;



public class UploadFileDto
{
    public required Stream Stream { get; set; }
    public required string FileName { get; set; }
    public required string FileType { get; set; }

    public long? FileSize { get; set; } // Agora é opcional
    public long FileLength { get; set; }
}
