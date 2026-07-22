namespace DataService.API.Models;

public class CartLineItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => Quantity * UnitPrice;
}
