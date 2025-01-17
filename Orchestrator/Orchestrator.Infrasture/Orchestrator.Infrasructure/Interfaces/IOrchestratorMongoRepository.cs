using Orchestrator.Domain.DTOs;

namespace Orchestrator.Infrasture.Persistence;

public interface IOrchestratorMongoRepository
{
    Task AddOrderAsync(OrderDTO order);
}