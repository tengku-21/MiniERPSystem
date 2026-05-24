namespace Inventory.Application.Interfaces;

public interface IInventoryRepository
{
    Task AddAsync(Inventory inventory);
    Task<Inventory?> GetByProductIdAsync(Guid productId);
    Task UpdateAsync(Inventory inventory);
}