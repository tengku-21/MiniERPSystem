namespace Orders.Application.DTOs;

public record CreateOrderRequest
{
    public List<OrderItemRequest> Items { get; set; } = new();
}

public record OrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}