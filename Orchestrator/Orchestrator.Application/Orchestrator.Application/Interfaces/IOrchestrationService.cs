using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Services;

public interface IOrchestrationService
{
    Task<float> HandleOrderCreatedAsync(UserChoice order);
    Task<float> StartOrderProcessAsync(UserChoice order);
    Task<float> HandleOrderProcessAsync(UserChoice order);

}