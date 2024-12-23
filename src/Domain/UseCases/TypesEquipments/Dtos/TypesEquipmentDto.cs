namespace Api.Domain.UseCases.TypesEquipments.Dtos;

public class TypesTypesEquipmentDto
{
    public int eqTypId { get; set; }
    [Required]
    public required string eqTypName { get; set; }
    public required string status { get; set; }
    public bool IsDeleted { get; set; }
}