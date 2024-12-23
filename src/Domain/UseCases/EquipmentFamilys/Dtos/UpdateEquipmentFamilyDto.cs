namespace Api.Domain.UseCases.EquipmentFamilys.Dtos;

public class UpdateEquipmentFamilyDto
{
    public int eqFamilyId { get; set; }
    public required string eqFamilyName { get; set; }
    public required string status { get; set; }
}