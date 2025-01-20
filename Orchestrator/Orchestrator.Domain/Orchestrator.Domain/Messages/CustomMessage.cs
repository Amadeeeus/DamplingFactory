namespace Orchestrator.Domain.Messages;

public class CustomMessage
{
    public string MessageId = new Guid().ToString();
    public string Content { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}