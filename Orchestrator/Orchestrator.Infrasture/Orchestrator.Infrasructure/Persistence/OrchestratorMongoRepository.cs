using AutoMapper;
using MongoDB.Driver;
using Orchestrator.Domain.DTOs;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Infrasture.Persistence;

public class OrchestratorMongoRepository : IOrchestratorMongoRepository
{
    private readonly IMongoCollection<MongoOrder> _mongo;
    private readonly IMapper _mapper;

    public OrchestratorMongoRepository(IMongoCollection<MongoOrder> mongo, IMapper mapper)
    {
        _mongo = mongo;
        _mapper = mapper;
    }

    public async Task AddOrderAsync(OrderDTO order)
    {
       var map = _mapper.Map<OrderDTO, MongoOrder>(order);
       await _mongo.InsertOneAsync(map);
    }
    
}