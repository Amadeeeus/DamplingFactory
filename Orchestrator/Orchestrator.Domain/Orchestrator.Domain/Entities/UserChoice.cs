namespace Orchestrator.Domain.Entities;

public class UserChoice
{
    public string? UserId { get; set; }
    public string? RecipeId { get; set; }
    public int PortionsCount { get; set; }
}