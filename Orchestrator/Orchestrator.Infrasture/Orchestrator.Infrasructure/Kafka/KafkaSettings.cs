namespace Orchestrator.Infrasructure.Kafka;

public class KafkaSettings
{
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
    public Topics Topics { get; set; }
    public int Timeout { get; set; }
}