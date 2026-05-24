using Microsoft.EntityFrameworkCore;
using Products.Application;
using Products.Application.Interfaces;
using Products.Infrastructure.Database;

namespace Products.Infrastructure.Repositories;

public class ProductRepository(ProductsDbContext db) : IProductRepository
{
    private readonly ProductsDbContext _db = db;

    public async Task AddAsync(Product product)
    {
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.ToListAsync();
    }
}