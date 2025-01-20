using Orchestrator.Domain.Enums;

namespace Orchestrator.Domain.Entities;

public class Order
{
    public Order()
    {
        Status = new OrderStatus{OrderId = Id, ProcessStage = ProcessStage.InProgress,OrderDate  = DateTime.UtcNow.AddMinutes(20)};
    }

    public string Id { get; } = new Guid().ToString();
    public string RecipeId { get; set; }
    public int Portions { get; set; }
    //can be float
    public float UserRating { get; set; }
    public string UserId { get; set; }
    public OrderStatus Status { get; set; }
}