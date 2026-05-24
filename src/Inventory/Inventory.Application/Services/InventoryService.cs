using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class InventoryService(IInventoryRepository repository) : IInventoryService
{
    private readonly IInventoryRepository _repository = repository;

    public async Task<StockResponseDTO> AddStockAsync(Guid productId, AdjustStockRequestDTO request)
    {
        var inventory = await _repository.GetByProductIdAsync(productId) ?? throw new InvalidOperationException($"No stock record found for product {productId}.");
        
        inventory.Quantity  += request.Quantity;
        inventory.UpdatedAt  = DateTime.UtcNow;

        await _repository.UpdateAsync(inventory);
        return MapToResponse(inventory);
    }

    public async Task<bool> DeductStockAsync(Guid productId, int quantity)
    {
        var inventory = await _repository.GetByProductIdAsync(productId);
        if (inventory is null || inventory.Quantity < quantity)
            return false;

        inventory.Quantity  -= quantity;
        inventory.UpdatedAt  = DateTime.UtcNow;

        await _repository.UpdateAsync(inventory);
        return true;
    }

    public async Task<StockResponseDTO?> GetStockAsync(Guid productId)
    {
        var inventory = await _repository.GetByProductIdAsync(productId);
        return inventory is null ? null : MapToResponse(inventory);
    }

    public async Task<StockResponseDTO> InitializeStockAsync(InitializeStockRequestDTO request)
    {
        var existing = await _repository.GetByProductIdAsync(request.ProductId);
        if (existing is not null)
            throw new InvalidOperationException($"Stock for product {request.ProductId} already exists.");

        var inventory = new Inventory
        {
            Id        = Guid.NewGuid(),
            ProductId = request.ProductId,
            Quantity  = request.Quantity,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(inventory);
        return MapToResponse(inventory);
    }

      private static StockResponseDTO MapToResponse(Inventory inventory) => new()
    {
        Id        = inventory.Id,
        ProductId = inventory.ProductId,
        Quantity  = inventory.Quantity,
        UpdatedAt = inventory.UpdatedAt
    };
}