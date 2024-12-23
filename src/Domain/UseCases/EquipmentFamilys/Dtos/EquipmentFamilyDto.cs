namespace Api.Domain.UseCases.EquipmentFamilys.Dtos;

public class EquipmentFamilyDto
{
    public int eqFamilyId { get; set; }
    [Required]
    public required string eqFamilyName { get; set; }
    public required string status { get; set; }
    public bool IsDeleted { get; set; }
}