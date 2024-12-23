namespace Api.Domain.UseCases.TypesCheklists.Dtos;

public class TypesTypesChecklistDto
{
    public int chTypId { get; set; }
    [Required]
    public required string chTypName { get; set; }
    public required string chTypStatus { get; set; }
    public bool IsDeleted { get; set; }
}