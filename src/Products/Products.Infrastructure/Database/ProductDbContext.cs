using Microsoft.EntityFrameworkCore;
using Products.Application;

namespace Products.Infrastructure.Database;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.SKU).IsUnique();
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });
    }
}