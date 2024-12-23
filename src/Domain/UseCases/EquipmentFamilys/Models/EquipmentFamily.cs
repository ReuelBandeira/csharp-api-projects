using Api.Shared.Bases;

namespace Api.Domain.UseCases.EquipmentFamilys.Models;

[Table("equipment_family")]
public class EquipmentFamily : AuditedBase
{
    [Key]
    public int eqFamId { get; set; }

    [Required]
    [MaxLength(120)]
    public required string eqFamName { get; set; }

    [Required]
    [MaxLength(10)]
    public required string status { get; set; }
}