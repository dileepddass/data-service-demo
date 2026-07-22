using DataService.UI.Models;
using System.Net.Http.Json;

namespace DataService.UI.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = await _httpClient.GetFromJsonAsync<IEnumerable<Order>>("api/orders");
        return orders ?? Enumerable.Empty<Order>();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Order>($"api/orders/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<Order?> CreateAsync(Order order)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", order);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Order>();
    }

    public async Task<Order?> UpdateAsync(int id, Order order)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}", order);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Order>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/orders/{id}");
        return response.IsSuccessStatusCode;
    }
}
