using Microsoft.EntityFrameworkCore;
using Orders.Application;

namespace Orders.Infrastructure.Database;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Status).HasConversion<string>();
            entity.HasMany(o => o.Items)
                  .WithOne()
                  .HasForeignKey(i => i.OrderId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
        });
    }
}