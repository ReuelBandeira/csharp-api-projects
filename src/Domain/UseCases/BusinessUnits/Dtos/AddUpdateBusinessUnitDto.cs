namespace Api.Domain.UseCases.BusinessUnits.Dtos;

public class AddUpdateBusinessUnitDto
{
    public int busId { get; set; }
    [Required]
    public required string busName { get; set; }
}