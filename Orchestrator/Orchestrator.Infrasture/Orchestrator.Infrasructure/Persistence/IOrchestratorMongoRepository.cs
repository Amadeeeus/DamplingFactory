using Orchestrator.Domain.Entities;

namespace Orchestrator.Infrasructure.Persistence;

public interface IOrchestratorMongoRepository
{
    Task AddOrderAsync(Order order);
    Task DeleteAllAsync();
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetLogsByOrderIdAsync(string orderId);
    Task DeleteLogsByIdAsync(string orderId);
}