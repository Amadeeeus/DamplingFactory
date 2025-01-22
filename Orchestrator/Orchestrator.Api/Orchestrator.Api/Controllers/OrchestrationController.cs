using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Application.Services;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrchestrationController:ControllerBase
{
    private readonly IOrchestrationService _service;

    public OrchestrationController(IOrchestrationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] UserChoice order)
    {
        await _service.HandleOrderCreatedAsync(order);
        return Ok();
    }
}