using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orchestrator.Domain.Interfaces;

public class KafkaConsumerService : IKafkaConsumerService
{
    private readonly ConsumerConfig _consumerConfig;

    private readonly ILogger _logger;

    public KafkaConsumerService(ConsumerConfig consumerConfig, ILogger logger)
    {
        _consumerConfig = consumerConfig;

        _logger = logger;
    }

    public async Task<T> ConsumeAsync<T>(string topic, CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        consumer.Subscribe(topic);
        var consumeResult = await Task.Run(() => consumer.Consume(cancellationToken),cancellationToken);
        return JsonConvert.DeserializeObject<T>(consumeResult.Message.Value)!;
    }
}
