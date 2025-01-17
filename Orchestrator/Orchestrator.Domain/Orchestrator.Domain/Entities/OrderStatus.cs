using Orchestrator.Domain.Enums;

namespace Orchestrator.Domain.Entities;

public class OrderStatus
{
    public Guid OrderId { get; set; }
    public ProcessStage ProcessStage { get; set; }
}