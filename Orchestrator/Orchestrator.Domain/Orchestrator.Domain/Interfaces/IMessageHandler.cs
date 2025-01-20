using Orchestrator.Domain.Messages;

namespace Orchestrator.Domain.Handlers;

public interface IMessageHandler<TMessage>
{
    Task<CustomMessage> HandleAsync(string message,CancellationToken cancellationToken);
}