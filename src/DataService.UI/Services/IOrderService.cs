using DataService.UI.Models;

namespace DataService.UI.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order?> CreateAsync(Order order);
    Task<Order?> UpdateAsync(int id, Order order);
    Task<bool> DeleteAsync(int id);
}
