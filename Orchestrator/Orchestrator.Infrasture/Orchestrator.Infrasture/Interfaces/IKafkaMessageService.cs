using System.Threading;
using System.Threading.Tasks;
using Orchestrator.Domain.Enums;

namespace Orchestrator.Infrasructure.Kafka;

public interface IKafkaMessageService
{
    Task<T> GetDataFromKafka<T>(string input, string output, AdminMessages messages, CancellationToken ct);
    Task<T> GetDataFromKafka<T>(string input, string output, string messages, CancellationToken ct);
    Task AddDataFromKafka<T>(string input, string output, T messages,CancellationToken ct);
    Task DeleteDataFromKafka(string input, string output, string name, CancellationToken ct);
}