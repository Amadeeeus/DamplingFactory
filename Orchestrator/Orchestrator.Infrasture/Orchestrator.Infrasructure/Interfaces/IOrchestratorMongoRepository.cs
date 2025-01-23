using Orchestrator.Domain.DTOs;

namespace Orchestrator.Infrasructure.Persistence;

public interface IOrchestratorMongoRepository
{
    Task AddOrderAsync(OrderDTO order);
}