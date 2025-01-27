namespace Orchestrator.Domain.Entities;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public HashSet<string> Ingredients { get; set; } = new ();
    public float Rating { get; set; }
}