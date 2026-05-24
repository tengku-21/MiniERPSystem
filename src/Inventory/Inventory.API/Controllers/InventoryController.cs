using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers;

[ApiController]
[Route("api/inventory")]
public class InventoryController(IInventoryService inventoryService) : ControllerBase
{
    private readonly IInventoryService _inventoryService = inventoryService;

    [HttpPost]
    public async Task<IActionResult> Initialize([FromBody] InitializeStockRequestDTO request)
    {
        var stock = await _inventoryService.InitializeStockAsync(request);
        return CreatedAtAction(nameof(GetStock), new { productId = stock.ProductId }, stock);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetStock(Guid productId)
    {
        var stock = await _inventoryService.GetStockAsync(productId);
        return stock is null ? NotFound() : Ok(stock);
    }

    [HttpPut("{productId}/add")]
    public async Task<IActionResult> AddStock(Guid productId, [FromBody] AdjustStockRequestDTO request)
    {
        var stock = await _inventoryService.AddStockAsync(productId, request);
        return Ok(stock);
    }
}