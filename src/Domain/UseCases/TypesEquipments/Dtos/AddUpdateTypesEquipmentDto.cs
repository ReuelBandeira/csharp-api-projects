namespace Api.Domain.UseCases.TypesEquipments.Dtos;

public class AddUpdateTypesEquipmentDto
{
    public int eqTypId { get; set; }
    [Required]
    public required string eqTypName { get; set; }
    public required string status { get; set; }

}