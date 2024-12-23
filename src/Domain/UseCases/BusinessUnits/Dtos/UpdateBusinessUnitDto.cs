namespace Api.Domain.UseCases.BusinessUnits.Dtos;

public class UpdateBusinessUnitDto
{
    public int busId { get; set; }
    public required string busName { get; set; }
}