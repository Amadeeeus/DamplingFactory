using System.Collections.Concurrent;
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
    private readonly ConcurrentDictionary<string, Order> _order = new();
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

    public async Task StartOrderProcessAsync(UserChoice order)
    {
        await _eventPublisher.PublishAsync("GetRecept", order.RecipeId);
        var recipe = await _consumer.ConsumeAsync<Recipe>("ReceptResponce", new CancellationToken());
        await GetRating(recipe);
        if (recipe.Ingredients.Count > 1)
        {
            await _eventPublisher.PublishAsync("GetCook", 3);
        }
        else
        {
           await _eventPublisher.PublishAsync("GetCook", 2);
        }
        var cookResponse = await _consumer.ConsumeAsync<Cook>("CookResponce", new CancellationToken());
        order.CookGrade = cookResponse.Grade;
         await HandleOrderProcessAsync(order);
    }

    public async Task HandleOrderProcessAsync(UserChoice order)
    {
        await _eventPublisher.PublishAsync("HotKitchen", order);
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var hotResponse = await _consumer.ConsumeAsync<int>("HotKitchenResponse", cts.Token);
        while (hotResponse != (int)CustomStatusCodes.Success)
        { 
            await (hotResponse switch
            {
                404 => Task.CompletedTask,
                311 =>  _eventPublisher.PublishAsync("GetCook", 1),
                312 => _eventPublisher.PublishAsync("ColdKitchen", 314),
                313 => _eventPublisher.PublishAsync("DoughKitchen", 315),
                _ => throw new InvalidOperationException($"Неизвестный ответ: {hotResponse}")
            });
        }
        await SwitchStatusToDone();
    }

    public Task SwitchStatusToDone()
    {
        _order.TryGetValue(orderId, out var currentOrder);
        var status = currentOrder!.Status;
        status.ProcessStage = ProcessStage.Done;
        currentOrder!.Status = status;
        _order[orderId] = currentOrder;
        return Task.CompletedTask;
    }

    public Task GetRating(Recipe recipe)
    {
        _order.TryGetValue(orderId,out var currentOrder);
        currentOrder!.UserRating = recipe.Rating;
        _order[orderId] = currentOrder;
        return Task.CompletedTask;
    }
}