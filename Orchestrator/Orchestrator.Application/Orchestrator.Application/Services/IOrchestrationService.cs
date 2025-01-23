using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Services;

public interface IOrchestrationService
{
    Task<float> HandleOrderCreatedAsync(UserChoice order);
    Task<float> StartOrderProcessAsync(UserChoice order);
    Task<float> HandleOrderProcessAsync(UserChoice order);
    Task<List<Order>> GetAllLogsAsync();
    Task<List<Cook>> GetAllCooksAsync();
    Task<List<Recipe>> GetAllReceiptsAsync();
    Task<Order> GetLogsByOrderIdAsync(string orderId);
    Task<Cook> GetCookByNameAsync(string name);
    Task<Recipe> GetReceiptByIdAsync(string name);
    Task AddCookAsync(Cook cook);
    Task AddReceiptAsync(Recipe recipe);
    Task DeleteCookAsync(string name);
    Task DeleteReceiptAsync(string id);
}