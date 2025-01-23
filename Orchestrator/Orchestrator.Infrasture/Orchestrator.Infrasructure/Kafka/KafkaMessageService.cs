using Orchestrator.Domain.Enums;
using Orchestrator.Domain.Interfaces;
using Orchestrator.Infrasructure.Persistence;

namespace Orchestrator.Infrasructure.Kafka;

public class KafkaMessageService : IKafkaMessageService
{
    private readonly IEventPublisher _eventPublisher;
    private readonly IKafkaConsumerService _consumer;

    public KafkaMessageService(IEventPublisher eventPublisher, IKafkaConsumerService consumer)
    {
        _eventPublisher = eventPublisher;
        _consumer = consumer;
    }

    public async Task<T> GetDataFromKafka<T>(string input, string output, AdminMessages messages, CancellationToken ct)
    {
        await _eventPublisher.PublishAsync(input, messages);
        return await _consumer.ConsumeAsync<T>(output, ct);
    }
    public async Task<T> GetDataFromKafka<T>(string input, string output, string messages, CancellationToken ct)
    {
        await _eventPublisher.PublishAsync(input, messages);
        return await _consumer.ConsumeAsync<T>(output, ct);
    }
    public async Task AddDataFromKafka<T>(string input, string output, T messages,CancellationToken ct)
    {
        await _eventPublisher.PublishAsync(input, messages);
        if (await _consumer.ConsumeAsync<int>(output, ct) != 200)
        {
            throw new Exception("Data already exists");
        }
    }

    public async Task DeleteDataFromKafka(string input, string output, string name, CancellationToken ct)
    {
        await _eventPublisher.PublishAsync(input, name);
        if (await _consumer.ConsumeAsync<int>(output, ct) != 200)
        {
            throw new Exception("Data not found");
        }
    }

}