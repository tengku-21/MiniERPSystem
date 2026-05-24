//Main inventory object

namespace Inventory.Application;

public class Inventory
{
    public Guid Id {get; set;}
    public Guid ProductId {get; set;}
    public int Quantity {get; set;}
    public DateTime UpdatedAt {get; set;}
}
