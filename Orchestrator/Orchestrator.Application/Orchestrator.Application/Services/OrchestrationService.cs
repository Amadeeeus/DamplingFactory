using AutoMapper;
using Orchestrator.Domain.Entities;
using Orchestrator.Domain.Enums;
using Orchestrator.Infrasture.Persistence;

namespace Orchestrator.Application.Services;

public class OrchestrationService : IOrchestrationService
{
    private readonly IOrchestratorMongoRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly Dictionary<Guid, OrderStatus> _order = new();
    private readonly IMapper _mapper;

    public OrchestrationService(IOrchestratorMongoRepository repository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleOrderCreatedAsync(Order order)
    {
        _order[order.Id] = new OrderStatus{OrderId = order.Id, ProcessStage = ProcessStage.InProgress};
        await _eventPublisher.PublishAsync("HotKitchenReadyCheck", new { OrderId = order.Id, ProcessStage = ProcessStage.InProgress });
    }
}
