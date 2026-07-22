using DataService.API.Models;

namespace DataService.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new()
    {
        new Order { Id = 1, CustomerName = "Alice Johnson",  Product = "Laptop Pro 15",     Quantity = 1, UnitPrice = 1299.99m, OrderDate = DateTime.Now.AddDays(-10), Status = "Delivered"  },
        new Order { Id = 2, CustomerName = "Bob Smith",      Product = "Wireless Mouse",     Quantity = 3, UnitPrice = 29.99m,   OrderDate = DateTime.Now.AddDays(-7),  Status = "Shipped"    },
        new Order { Id = 3, CustomerName = "Carol White",    Product = "USB-C Hub",          Quantity = 2, UnitPrice = 49.95m,   OrderDate = DateTime.Now.AddDays(-5),  Status = "Processing" },
        new Order { Id = 4, CustomerName = "David Brown",    Product = "Mechanical Keyboard", Quantity = 1, UnitPrice = 149.00m, OrderDate = DateTime.Now.AddDays(-3),  Status = "Pending"    },
        new Order { Id = 5, CustomerName = "Eve Martinez",   Product = "27\" 4K Monitor",    Quantity = 2, UnitPrice = 549.00m,  OrderDate = DateTime.Now.AddDays(-1),  Status = "Pending"    },
    };

    private int _nextId = 6;

    public IEnumerable<Order> GetAll() => _orders.ToList();

    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public Order Create(Order order)
    {
        order.Id = _nextId++;
        order.OrderDate = DateTime.Now;
        _orders.Add(order);
        return order;
    }

    public Order? Update(int id, Order order)
    {
        var existing = _orders.FirstOrDefault(o => o.Id == id);
        if (existing is null) return null;

        existing.CustomerName = order.CustomerName;
        existing.Product = order.Product;
        existing.Quantity = order.Quantity;
        existing.UnitPrice = order.UnitPrice;
        existing.Status = order.Status;
        return existing;
    }

    public bool Delete(int id)
    {
        var existing = _orders.FirstOrDefault(o => o.Id == id);
        if (existing is null) return false;
        _orders.Remove(existing);
        return true;
    }
}
