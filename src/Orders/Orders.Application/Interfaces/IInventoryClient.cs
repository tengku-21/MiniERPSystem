using Orders.Application.DTOs;

namespace Orders.Application.Interfaces;

public interface IInventoryClient
{
    Task<StockDto?> GetStockAsync(Guid productId);
    Task<bool> DeductStockAsync(Guid productId, int quantity);
}