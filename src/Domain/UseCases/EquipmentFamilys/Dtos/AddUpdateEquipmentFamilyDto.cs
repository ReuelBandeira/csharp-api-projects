namespace Api.Domain.UseCases.EquipmentFamilys.Dtos;

public class AddUpdateEquipmentFamilyDto
{
    public int eqFamilyId { get; set; }
    [Required]
    public required string eqFamilyName { get; set; }
    public required string status { get; set; }

}