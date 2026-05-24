using Products.Application.DTOs;
using Products.Application.Interfaces;

namespace Products.Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Id          = Guid.NewGuid(),
            Name        = request.Name,
            SKU         = request.SKU,
            Description = request.Description,
            Price       = request.Price,
            CreatedAt   = DateTime.UtcNow
        };

        await _repository.AddAsync(product);
        return MapToResponse(product);
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product is null ? null : MapToResponse(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(MapToResponse);
    }

    private static ProductResponse MapToResponse(Product product) => new()
    {
        Id          = product.Id,
        Name        = product.Name,
        SKU         = product.SKU,
        Description = product.Description,
        Price       = product.Price,
        CreatedAt   = product.CreatedAt
    };
}