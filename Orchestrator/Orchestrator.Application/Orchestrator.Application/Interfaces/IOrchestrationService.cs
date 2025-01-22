using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Services;

public interface IOrchestrationService
{
    Task HandleOrderCreatedAsync(UserChoice order);
    Task StartOrderProcessAsync(UserChoice order);
    Task HandleOrderProcessAsync(UserChoice order);
    Task SwitchStatusToDone();
    Task GetRating(Recipe recipe);
}