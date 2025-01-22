namespace Orchestrator.Domain.Entities;

public class Cook
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; }
    public int Grade { get; set; }
}