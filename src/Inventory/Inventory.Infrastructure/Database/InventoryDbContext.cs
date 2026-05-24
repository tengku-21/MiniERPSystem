using Inventory.Application;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Database;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<Inventory.Application.Inventory> Inventories => Set<Inventory.Application.Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory.Application.Inventory>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.HasIndex(i => i.ProductId).IsUnique();
        });
    }
}