using Api.Shared.Bases;

namespace Api.Domain.UseCases.BusinessUnits.Models;

[Table("business_unit")]
public class BusinessUnit : AuditedBase
{
    [Key]
    public int busId { get; set; }

    [Required]
    [MaxLength(120)]
    public required string busName { get; set; }
}