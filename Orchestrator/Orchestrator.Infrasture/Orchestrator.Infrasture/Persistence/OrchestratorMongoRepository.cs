using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Infrasructure.Persistence;

public class OrchestratorMongoRepository : IOrchestratorMongoRepository
{
    private readonly IMongoCollection<Order> _mongo;
    private readonly IMapper _mapper;

    public OrchestratorMongoRepository(IMongoCollection<Order> mongo, IMapper mapper)
    {
        _mongo = mongo;
        _mapper = mapper;
    }

    public async Task AddOrderAsync(Order order)
    {
       await _mongo.InsertOneAsync(order);
    }

    public async Task DeleteAllAsync()
    {
        await _mongo.DeleteManyAsync(Builders<Order>.Filter.Empty);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _mongo.Find(Builders<Order>.Filter.Empty).ToListAsync();
    }

    public async Task<List<Order>> GetLogsByOrderIdAsync(string orderId)
    { 
        return await _mongo.Find(x => x.Id == orderId).ToListAsync(); 
    }

    public async Task DeleteLogsByIdAsync(string orderId)
    {
        await _mongo.DeleteManyAsync<Order>(x => x.Id == orderId);
    }
}