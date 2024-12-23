namespace Api.Domain.UseCases.TypesChecklists.Dtos;

public class UpdateTypesChecklistDto
{
    public int chTypId { get; set; }
    public required string chTypName { get; set; }
    public required string chTypStatus { get; set; }
}