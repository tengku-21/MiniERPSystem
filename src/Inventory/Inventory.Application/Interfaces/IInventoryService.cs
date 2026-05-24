using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces;

public interface IInventoryService
{
    Task<StockResponseDTO> InitializeStockAsync(InitializeStockRequestDTO request);
    Task<StockResponseDTO?> GetStockAsync(Guid productId);
    Task<StockResponseDTO> AddStockAsync(Guid productId, AdjustStockRequestDTO request);
    Task<bool> DeductStockAsync(Guid productId, int quantity);
}