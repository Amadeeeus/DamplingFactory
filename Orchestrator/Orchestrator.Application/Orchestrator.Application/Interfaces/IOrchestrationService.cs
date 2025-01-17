using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Services;

public interface IOrchestrationService
{
    Task HandleOrderCreatedAsync(Order order);
}