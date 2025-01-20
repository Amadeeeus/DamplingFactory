using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orchestrator.Domain.Messages;

namespace Orchestrator.Domain.Handlers;

public class OrchestratorMessageHandler:IMessageHandler<CustomMessage>
{
    private readonly ILogger _logger;

    public OrchestratorMessageHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<CustomMessage> HandleAsync(string message, CancellationToken token)
    {
        try
        {
            var deserializedMessage = await Task.Run(()=>JsonConvert.DeserializeObject<CustomMessage>(message), token);
            return  deserializedMessage!;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, ex.Message);
            return null!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return null!;
        }
    }
}