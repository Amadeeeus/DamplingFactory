using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Orchestrator.Infrasructure.HttpClients;

public static class HttpClients
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("CookService", client =>
        {
            client.BaseAddress = new Uri(configuration["localhost:5433"]!);
        });
        services.AddHttpClient("RecipeService", client =>
        {
            client.BaseAddress = new Uri(configuration["localhost:5434"]!);
        }); 
        services.AddHttpClient("DoughKitchen", client =>
        {
            client.BaseAddress = new Uri(configuration["localhost:5435"]!);
        });
        services.AddHttpClient("HotKitchen", client =>
        {
            client.BaseAddress = new Uri(configuration["localhost:5436"]!);
        });
        services.AddHttpClient("ColdKitchen", client =>
        {
            client.BaseAddress = new Uri(configuration["localhost:5437"]!);
        });
    }
}