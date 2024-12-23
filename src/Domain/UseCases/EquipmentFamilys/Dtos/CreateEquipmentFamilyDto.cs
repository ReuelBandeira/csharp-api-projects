namespace Api.Domain.UseCases.EquipmentFamilys.Dtos;

public class CreateEquipmentFamilyDto
{
    public int eqFamilyId { get; set; }
    public required string eqFamilyName { get; set; } = string.Empty;
    public required string status { get; set; } = string.Empty;
}