namespace Api.Domain.UseCases.BusinessUnits.Dtos;

public class BusinessUnitDto
{
    public int busId { get; set; }
    [Required]
    public required string busName { get; set; }
    public bool IsDeleted { get; set; }
}