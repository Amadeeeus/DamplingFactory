using Orchestrator.Domain.Enums;

namespace Orchestrator.Domain.Entities;

public class OrderStatus
{
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public ProcessStage ProcessStage { get; set; }
}