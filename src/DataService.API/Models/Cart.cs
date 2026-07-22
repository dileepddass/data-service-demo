namespace DataService.API.Models;

public class Cart
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Status { get; set; } = "Active";
    public List<CartLineItem> LineItems { get; set; } = new();
    public decimal TotalAmount => LineItems.Sum(li => li.LineTotal);
}
