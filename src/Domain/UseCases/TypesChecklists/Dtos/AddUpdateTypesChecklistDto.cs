namespace Api.Domain.UseCases.TypesChecklists.Dtos;

public class AddUpdateTypesChecklistDto
{
    public int chTypId { get; set; }
    [Required]
    public required string chTypName { get; set; }
    public required string chTypStatus { get; set; }

}