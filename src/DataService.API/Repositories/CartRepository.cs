using DataService.API.Models;

namespace DataService.API.Repositories;

public class CartRepository : ICartRepository
{
    private readonly List<Cart> _carts = new()
    {
        new Cart
        {
            Id = 1,
            CustomerName = "Alice Johnson",
            CreatedDate = DateTime.Now.AddDays(-5),
            Status = "Active",
            LineItems = new List<CartLineItem>
            {
                new() { Id = 1, CartId = 1, Product = "Laptop Pro 15", Quantity = 1, UnitPrice = 1299.99m },
                new() { Id = 2, CartId = 1, Product = "Wireless Mouse", Quantity = 2, UnitPrice = 29.99m },
            }
        },
        new Cart
        {
            Id = 2,
            CustomerName = "Bob Smith",
            CreatedDate = DateTime.Now.AddDays(-3),
            Status = "Active",
            LineItems = new List<CartLineItem>
            {
                new() { Id = 3, CartId = 2, Product = "USB-C Hub", Quantity = 1, UnitPrice = 49.95m },
            }
        },
        new Cart
        {
            Id = 3,
            CustomerName = "Carol White",
            CreatedDate = DateTime.Now.AddDays(-1),
            Status = "CheckedOut",
            LineItems = new List<CartLineItem>
            {
                new() { Id = 4, CartId = 3, Product = "Mechanical Keyboard", Quantity = 1, UnitPrice = 149.00m },
                new() { Id = 5, CartId = 3, Product = "27\" 4K Monitor", Quantity = 1, UnitPrice = 549.00m },
            }
        },
    };

    private int _nextCartId = 4;
    private int _nextLineItemId = 6;

    public IEnumerable<Cart> GetAll() => _carts.ToList();

    public Cart? GetById(int id) => _carts.FirstOrDefault(c => c.Id == id);

    public IEnumerable<Cart> Search(string? customerName, string? status)
    {
        var query = _carts.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(customerName))
        {
            query = query.Where(c => c.CustomerName.Contains(customerName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        return query.ToList();
    }

    public Cart Create(Cart cart)
    {
        cart.Id = _nextCartId++;
        cart.CreatedDate = DateTime.Now;
        cart.LineItems ??= new List<CartLineItem>();

        foreach (var lineItem in cart.LineItems)
        {
            lineItem.Id = _nextLineItemId++;
            lineItem.CartId = cart.Id;
        }

        _carts.Add(cart);
        return cart;
    }

    public Cart? Update(int id, Cart cart)
    {
        var existing = _carts.FirstOrDefault(c => c.Id == id);
        if (existing is null) return null;

        existing.CustomerName = cart.CustomerName;
        existing.Status = cart.Status;
        return existing;
    }

    public bool Delete(int id)
    {
        var existing = _carts.FirstOrDefault(c => c.Id == id);
        if (existing is null) return false;

        _carts.Remove(existing);
        return true;
    }

    public CartLineItem? AddLineItem(int cartId, CartLineItem lineItem)
    {
        var cart = _carts.FirstOrDefault(c => c.Id == cartId);
        if (cart is null) return null;

        lineItem.Id = _nextLineItemId++;
        lineItem.CartId = cartId;
        cart.LineItems.Add(lineItem);
        return lineItem;
    }

    public bool DeleteLineItem(int cartId, int lineItemId)
    {
        var cart = _carts.FirstOrDefault(c => c.Id == cartId);
        if (cart is null) return false;

        var lineItem = cart.LineItems.FirstOrDefault(li => li.Id == lineItemId);
        if (lineItem is null) return false;

        cart.LineItems.Remove(lineItem);
        return true;
    }
}
