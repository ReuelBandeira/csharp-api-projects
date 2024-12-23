namespace Api.Shared.Bases;

public abstract class AuditedBase
{
    public DateTime createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
}