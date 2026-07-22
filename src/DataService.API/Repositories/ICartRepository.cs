using DataService.API.Models;

namespace DataService.API.Repositories;

public interface ICartRepository
{
    IEnumerable<Cart> GetAll();
    Cart? GetById(int id);
    IEnumerable<Cart> Search(string? customerName, string? status);
    Cart Create(Cart cart);
    Cart? Update(int id, Cart cart);
    bool Delete(int id);
    CartLineItem? AddLineItem(int cartId, CartLineItem lineItem);
    bool DeleteLineItem(int cartId, int lineItemId);
}
