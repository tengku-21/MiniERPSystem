using Products.Application.DTOs;

namespace Products.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponse> CreateAsync(CreateProductRequest request);
    Task<ProductResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllAsync();
}