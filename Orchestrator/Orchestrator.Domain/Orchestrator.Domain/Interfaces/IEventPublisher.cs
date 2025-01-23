namespace Orchestrator.Infrasructure.Persistence;

public interface IEventPublisher
{ 
    Task PublishAsync<T>(string? topic, T message);
}