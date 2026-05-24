using Inventory.Application;
using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class InventoryRepository(InventoryDbContext db) : IInventoryRepository
{
    private readonly InventoryDbContext _db = db;

    public async Task AddAsync(Inventory.Application.Inventory inventory)
    {
        await _db.Inventories.AddAsync(inventory);
        await _db.SaveChangesAsync();
    }

    public async Task<Inventory.Application.Inventory?> GetByProductIdAsync(Guid productId)
    {
        return await _db.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task UpdateAsync(Inventory.Application.Inventory inventory)
    {
        _db.Inventories.Update(inventory);
        await _db.SaveChangesAsync();
    }
}