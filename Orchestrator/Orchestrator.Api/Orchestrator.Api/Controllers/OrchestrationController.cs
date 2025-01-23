using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Application.Services;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Api.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
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
    public async Task<ActionResult> GetAllLogsAsync()
    {
        return Ok(await _service.GetAllLogsAsync());
    }

    public async Task<IActionResult> GetAllCooksAsync()
    {
        return Ok(await _service.GetAllCooksAsync());
    }

    public async Task<IActionResult> GetAllReceiptsAsync()
    {
        return Ok(await _service.GetAllReceiptsAsync());
    }

    public async Task<IActionResult> GetLogByOrderIdAsync(string orderId)
    {
        return Ok(await _service.GetLogsByOrderIdAsync(orderId));
    }

    public async Task<IActionResult> GetCookByNameAsync(string name)
    {
        return Ok(await _service.GetCookByNameAsync(name));
    }

    public async Task<IActionResult> GetReceiptByNameAsync(string name)
    {
        return Ok(await _service.GetReceiptByIdAsync(name));
    }

    public async Task<IActionResult> AddCookAsync(Cook cook)
    {
        await _service.AddCookAsync(cook);
        return Ok();
    }
    
    public async Task<IActionResult> AddReceiptAsync(Recipe recipe)
    {
        await _service.AddReceiptAsync(recipe);
        return Ok();
    }

    public async Task<IActionResult> DeleteCookAsync(string name)
    {
        await _service.DeleteCookAsync(name);
        return Ok();
    }

    public async Task<IActionResult> DeleteReceiptAsync(string name)
    {
        await _service.DeleteReceiptAsync(name);
        return Ok();
    }
}