namespace Orchestrator.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpClient("CookService", client =>
        {
            client.BaseAddress = new Uri(_configuration["localhost:5433"]!);
        });
        services.AddHttpClient("RecipeService", client =>
        {
            client.BaseAddress = new Uri(_configuration["localhost:5434"]!);
        }); 
        services.AddHttpClient("DoughKitchen", client =>
        {
            client.BaseAddress = new Uri(_configuration["localhost:5435"]!);
        });
        services.AddHttpClient("HotKitchen", client =>
        {
            client.BaseAddress = new Uri(_configuration["localhost:5436"]!);
        });
        services.AddHttpClient("ColdKitchen", client =>
        {
            client.BaseAddress = new Uri(_configuration["localhost:5437"]!);
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddApplicationServices();
        services.AddInfrastructureServices(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c=> c.SwaggerEndpoint("/swagger/v/1/swagger.json", "Orchestrator.Api v1"));
        }
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(en=>
        {
            en.MapControllers();
        });
    }
}