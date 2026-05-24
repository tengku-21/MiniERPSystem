//main order object (DB)

namespace Orders.Application;

public class Order
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

public enum OrderStatus
{
    Pending,
    Confirmed,
    Failed
}