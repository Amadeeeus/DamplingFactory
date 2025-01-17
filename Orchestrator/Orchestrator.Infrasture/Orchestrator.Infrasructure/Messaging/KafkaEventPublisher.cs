using Confluent.Kafka;
using Orchestrator.Infrasture.Persistence;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Orchestrator.Infrasture.Messaging;

public class KafkaEventPublisher:IEventPublisher
{
    private readonly IProducer<Null, string> _producer;

    public KafkaEventPublisher()
    {
        var config = new ProducerConfig{ BootstrapServers = "localhost:9092" };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishAsync(string topic, object message)
    {
        await _producer.ProduceAsync(topic, new Message<Null, string>{Value = JsonConvert.SerializeObject(message)});
    }
}