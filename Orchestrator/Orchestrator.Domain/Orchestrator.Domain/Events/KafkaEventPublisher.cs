using Confluent.Kafka;
using Orchestrator.Infrasructure.Persistence;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Orchestrator.Domain.Events;

public class KafkaEventPublisher:IEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly Dictionary<string, Func<string, Task>> _handlers = new ();

    public KafkaEventPublisher()
    {
        var config = new ProducerConfig{ BootstrapServers = "localhost:9092" };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync<T>(string? topic, T message)
    {
        await _producer.ProduceAsync(topic, new Message<string, string>{Value = JsonConvert.SerializeObject(message)});
    }
}