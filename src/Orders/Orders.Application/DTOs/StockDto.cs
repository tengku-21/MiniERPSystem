namespace Orders.Application.DTOs;

public record StockDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}