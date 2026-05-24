using Orders.Application.DTOs;

namespace Orders.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateAsync(CreateOrderRequest request);
    Task<OrderResponse?> GetByIdAsync(Guid id);
}