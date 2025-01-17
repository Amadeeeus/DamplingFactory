using AutoMapper;
using Orchestrator.Domain.Entities;
using Orchestrator.Infrasture.Persistence;

namespace Orchestrator.Application.Services;

public class OrchestrationService : IOrchestrationService
{
    private readonly IOrchestratorMongoRepository _repository;
    private readonly IMapper _mapper;

    public OrchestrationService(IOrchestratorMongoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleOrderCreatedAsync(Order order)
    {
        
    }
}
