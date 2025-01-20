namespace Orchestrator.Infrasture.Persistence;

public interface IEventPublisher
{ 
    Task PublishAsync<T>(string topic, T message);
}