namespace Products.Application.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
}