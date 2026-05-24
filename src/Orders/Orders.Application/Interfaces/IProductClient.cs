using Orders.Application.DTOs;

namespace Orders.Application.Interfaces;

public interface IProductClient
{
    Task<ProductDto?> GetProductAsync(Guid productId);
}