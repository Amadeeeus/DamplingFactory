using Orchestrator.Domain.Enums;

namespace Orchestrator.Domain.Entities;

public class Order
{
    public Order()
    {
        Status = new OrderStatus{OrderId = Id, ProcessStage = ProcessStage.InProgress};
    }

    public Guid Id { get; } = new ();
    public Guid DamplingId { get; set; }
    public int Portions { get; set; }
    public float UserRating { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow.AddMinutes(20);
    public OrderStatus Status { get; set; }
}