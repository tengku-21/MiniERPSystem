namespace Inventory.Application.DTOs;

public record InitializeStockRequestDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}