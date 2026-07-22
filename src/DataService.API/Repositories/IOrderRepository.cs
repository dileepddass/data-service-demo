using DataService.API.Models;

namespace DataService.API.Repositories;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    Order? GetById(int id);
    Order Create(Order order);
    Order? Update(int id, Order order);
    bool Delete(int id);
}
