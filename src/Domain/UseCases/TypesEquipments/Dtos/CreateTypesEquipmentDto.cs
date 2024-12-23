namespace Api.Domain.UseCases.TypesEquipments.Dtos;

public class CreateTypesEquipmentDto
{
    public int eqTypId { get; set; }
    public required string eqTypName { get; set; } = string.Empty;
    public required string status { get; set; } = string.Empty;
}