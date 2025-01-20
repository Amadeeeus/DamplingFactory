namespace Orchestrator.Domain.Interfaces;

public interface IKafkaConsumerService
{
    Task<T> ConsumeAsync<T>(string topic,CancellationToken cancellationToken);
}