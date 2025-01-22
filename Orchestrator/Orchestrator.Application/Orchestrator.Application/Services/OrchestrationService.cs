using System.Collections.Concurrent;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Orchestrator.Domain.Entities;
using Orchestrator.Domain.Enums;
using Orchestrator.Infrasture.Kafka;
using Orchestrator.Infrasture.Persistence;

namespace Orchestrator.Application.Services;

public class OrchestrationService : IOrchestrationService
{
    private readonly IOrchestratorMongoRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly KafkaConsumerService _consumer;
    private readonly KafkaSettings _settings;
    private readonly ConcurrentDictionary<string, Order> _order = new();
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;
    private string _orderId; 

    public OrchestrationService(IOrchestratorMongoRepository repository, IMapper mapper, IEventPublisher eventPublisher,
        KafkaConsumerService consumer, IHttpContextAccessor accessor, IOptions<KafkaSettings> options, string orderId)
    {
        _eventPublisher = eventPublisher;
        _accessor = accessor;
        _orderId = orderId;
        _consumer = consumer;
        _repository = repository;
        _mapper = mapper;
        _settings = options.Value;
    }

    public async Task<float> HandleOrderCreatedAsync(UserChoice order)
    {
        var status = new OrderStatus { OrderId = new Guid().ToString(), ProcessStage = ProcessStage.InProgress };
        _orderId = status.OrderId;
        _order[status.OrderId] = new Order
        {
            UserId = _accessor.HttpContext.Request.Cookies["UserId"],
            RecipeId = order.RecipeId!,
            Portions = order.PortionsCount,
            Status = status
        };
        order.UserId = _accessor.HttpContext.Request.Cookies["UserId"];
        return await StartOrderProcessAsync(order);
    }

    public async Task<float> StartOrderProcessAsync(UserChoice order)
    {
        await _eventPublisher.PublishAsync(_settings.Topics.GetReceipt, order.RecipeId);
        var recipe = await _consumer.ConsumeAsync<Recipe>(_settings.Topics.ReceiptResponse, await GetCt());
        await GetRating(recipe);
        if (recipe.Ingredients.Count > 1)
        {
            await _eventPublisher.PublishAsync(_settings.Topics.GetCook, 3);
        }
        else
        {
           await _eventPublisher.PublishAsync(_settings.Topics.GetCook, 2);
        }
        var cookResponse = await _consumer.ConsumeAsync<Cook>(_settings.Topics.CookResponse, await GetCt());
        order.CookGrade = cookResponse.Grade;
        return await HandleOrderProcessAsync(order);
    }

    public async Task<float> HandleOrderProcessAsync(UserChoice order)
    {
        await _eventPublisher.PublishAsync(_settings.Topics.HotKitchen, order);
        var hotResponse = await _consumer.ConsumeAsync<int>(_settings.Topics.HotKitchenResponse, await GetCt());
        if (hotResponse != 200)
        {
            await _eventPublisher.PublishAsync(_settings.Topics.ColdKitchen, 312);
            var coldResponse = await _consumer.ConsumeAsync<int>(_settings.Topics.ColdKitchenResponse, await GetCt());
            if (coldResponse != 200)
            {
                await _eventPublisher.PublishAsync(_settings.Topics.DoughKitchen, 312);
                var doughResponse =await _consumer.ConsumeAsync<int>(_settings.Topics.DoughKitchenResponse, await GetCt());
                while (doughResponse != 200)
                {
                    await _eventPublisher.PublishAsync(_settings.Topics.GetCook, 1);
                    doughResponse = await _consumer.ConsumeAsync<int>(_settings.Topics.GetCook, await GetCt());
                }
            }
        }
        await SwitchStatusToDone();
        return _order[_orderId].UserRating;
    }

    private Task SwitchStatusToDone()
    {
        _order.TryGetValue(_orderId, out var currentOrder);
        var status = currentOrder!.Status;
        status.ProcessStage = ProcessStage.Done;
        currentOrder!.Status = status;
        _order[_orderId] = currentOrder;
        return Task.CompletedTask;
    }

    private Task GetRating(Recipe recipe)
    {
        _order.TryGetValue(_orderId,out var currentOrder);
        currentOrder!.UserRating = recipe.Rating;
        _order[_orderId] = currentOrder;
        return Task.CompletedTask;
    }

    private Task<CancellationToken> GetCt()
    {
        var cts = new CancellationTokenSource(_settings.Timeout);
        return Task.FromResult(cts.Token);
    }
}    
