namespace Api.Domain.UseCases.TypesChecklists.Dtos;

public class CreateTypesChecklistDto
{
    public int chTypId { get; set; }
    public required string chTypName { get; set; } = string.Empty;
    public required string chTypStatus { get; set; } = string.Empty;
}