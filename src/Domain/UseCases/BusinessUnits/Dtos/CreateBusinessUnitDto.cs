namespace Api.Domain.UseCases.BusinessUnits.Dtos;

public class CreateBusinessUnitDto
{
    public int busId { get; set; }
    public required string busName { get; set; } = string.Empty;
}