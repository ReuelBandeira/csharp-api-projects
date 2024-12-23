namespace Api.Domain.UseCases.TypesEquipments.Dtos;

public class UpdateTypesEquipmentDto
{
    public int eqTypId { get; set; }
    public required string eqTypName { get; set; }
    public required string status { get; set; }
}