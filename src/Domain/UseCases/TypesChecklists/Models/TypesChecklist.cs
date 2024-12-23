using Api.Shared.Bases;

namespace Api.Domain.UseCases.TypesChecklists.Models;

[Table("types_checklist")]
public class TypesChecklist : AuditedBase
{
    [Key]
    public int chTypId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string chTypName { get; set; }

    [Required]
    [MaxLength(10)]
    public required string chTypStatus { get; set; }
}