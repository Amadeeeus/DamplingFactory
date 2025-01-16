using Orchestrator.Application.Services;
using Orchestrator.Infrasture.HttpClients;

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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IOrchestratorService, OrchestrationService>();
        services.AddHttpClients(_configuration);
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