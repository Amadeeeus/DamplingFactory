using Orchestrator.Domain.Enums;

namespace Orchestrator.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = new Guid();
    public Guid DamplingId { get; set; }
    public int Portions { get; set; }
    public float UserRating { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow.AddMinutes(20);
    public ProcessStage ProcessStage { get; set; } = ProcessStage.InProgress;
}