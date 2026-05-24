using Orders.Application.DTOs;
using Orders.Application.Interfaces;

namespace Orders.Application.Services;

public class OrderService(
    IOrderRepository repository,
    IProductClient productClient,
    IInventoryClient inventoryClient) : IOrderService
{
    private readonly IOrderRepository _repository = repository;
    private readonly IProductClient _productClient = productClient;
    private readonly IInventoryClient _inventoryClient = inventoryClient;

    public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
    {
        var order = new Order
        {
            Id        = Guid.NewGuid(),
            Status    = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            Items     = new List<OrderItem>()
        };

        // First validate every item before touching anything
        foreach (var item in request.Items)
        {
            // check product exists
            var product = await _productClient.GetProductAsync(item.ProductId);
            if (product is null)
            {
                order.Status = OrderStatus.Failed;
                await _repository.AddAsync(order);
                throw new InvalidOperationException($"Product {item.ProductId} not found.");
            }

            // check stock available
            var stock = await _inventoryClient.GetStockAsync(item.ProductId);
            if (stock is null || stock.Quantity < item.Quantity)
            {
                order.Status = OrderStatus.Failed;
                await _repository.AddAsync(order);
                throw new InvalidOperationException(
                    $"Insufficient stock for product {product.Name}. " +
                    $"Requested: {item.Quantity}, Available: {stock?.Quantity ?? 0}.");
            }

            order.Items.Add(new OrderItem
            {
                Id        = Guid.NewGuid(),
                OrderId   = order.Id,
                ProductId = item.ProductId,
                Quantity  = item.Quantity,
                UnitPrice = product.Price  // snapshot price at time of order
            });
        }

        // 2. all items valid, now deduct stock
        foreach (var item in order.Items)
        {
            var deducted = await _inventoryClient.DeductStockAsync(item.ProductId, item.Quantity);
            if (!deducted)
            {
                order.Status = OrderStatus.Failed;
                await _repository.AddAsync(order);
                throw new InvalidOperationException(
                    $"Failed to deduct stock for product {item.ProductId}.");
            }
        }

        // 3. if all good, confirm order
        order.Status = OrderStatus.Confirmed;
        await _repository.AddAsync(order);
        return MapToResponse(order);
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        return order is null ? null : MapToResponse(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();
        return orders.Select(MapToResponse);
    }

    private static OrderResponse MapToResponse(Order order) => new()
    {
        Id        = order.Id,
        Status    = order.Status.ToString(),
        CreatedAt = order.CreatedAt,
        Items     = order.Items.Select(i => new OrderItemResponse
        {
            ProductId = i.ProductId,
            Quantity  = i.Quantity,
            UnitPrice = i.UnitPrice
        }).ToList()
    };
}