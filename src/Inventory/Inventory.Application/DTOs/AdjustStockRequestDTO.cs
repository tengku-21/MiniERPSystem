namespace Inventory.Application.DTOs;

public record AdjustStockRequestDTO
{
    public int Quantity { get; set; }
}