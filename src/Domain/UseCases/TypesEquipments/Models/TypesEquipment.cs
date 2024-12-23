using Api.Shared.Bases;

namespace Api.Domain.UseCases.TypesEquipments.Models;

[Table("types_equipment")]
public class TypesEquipment : AuditedBase
{
    [Key]
    public int eqTypId { get; set; }

    [Required]
    [MaxLength(120)]
    public required string eqTypName { get; set; }

    [Required]
    [MaxLength(10)]
    public required string status { get; set; }
}