using AutoMapper;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Orchestrator.Domain.Entities;
using Orchestrator.Domain.Enums;
using Orchestrator.Domain.Events;
using Orchestrator.Domain.Interfaces;
using Orchestrator.Domain.Messages;
using Orchestrator.Infrasture.Persistence;

namespace Orchestrator.Application.Services;

public class OrchestrationService : IOrchestrationService
{
    private readonly IOrchestratorMongoRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly KafkaConsumerService _consumer;
    private readonly Dictionary<string, Order> _order = new();
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;
    private string orderId; 

    public OrchestrationService(IOrchestratorMongoRepository repository, IMapper mapper, IEventPublisher eventPublisher,
        KafkaConsumerService consumer, IHttpContextAccessor accessor)
    {
        _eventPublisher = eventPublisher;
        _accessor = accessor;
        _consumer = consumer;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleOrderCreatedAsync(UserChoice order)
    {
        var status = new OrderStatus { OrderId = new Guid().ToString(), ProcessStage = ProcessStage.InProgress };
        orderId = status.OrderId;
        _order[status.OrderId] = new Order
        {
            UserId = _accessor.HttpContext.Request.Cookies["UserId"],
            RecipeId = order.RecipeId!,
            Portions = order.PortionsCount,
            Status = status
        };
        order.UserId = _accessor.HttpContext.Request.Cookies["UserId"];
        await StartOrderProcessAsync(order);
    }

    private async Task StartOrderProcessAsync(UserChoice order)
    {
        _eventPublisher.PublishAsync("GetRecept", order.RecipeId);
        var recipe = await _consumer.ConsumeAsync<Recipe>("ReceptResponce", new CancellationToken());
        var currentOrder = new Order();
        _order.TryGetValue(orderId,out currentOrder);
        if (recipe.Ingredients.Count > 1)
        {
            _eventPublisher.PublishAsync("GetCook", 3);
        }
        else
        {
            _eventPublisher.PublishAsync("GetCook", 2);
        }
        
    }

    private async Task HandleOrderProcessAsync(UserChoice order)
    private string DetermineTargetTopicBasedOnService(string sourceService)
    {
        return sourceService switch
        {
            "UserService" => "UserTopic",
            "RatingService" => "RatingTopic",
            "OrderService" => "OrderTopic",
            _ => throw new InvalidOperationException($"Неизвестный источник: {sourceService}")
        };
    }
}