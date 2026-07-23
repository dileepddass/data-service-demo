using DataService.UI.Models;
using DataService.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataService.UI.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET /Orders
    public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 5)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 5;

        var allOrders = (await _orderService.GetAllAsync()).ToList();

        var filtered = string.IsNullOrWhiteSpace(search)
            ? allOrders
            : allOrders.Where(o =>
                o.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                o.Product.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                o.Status.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();

        var totalCount = filtered.Count;
        var totalPages = pageSize > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 0;

        if (totalPages > 0 && page > totalPages)
            page = totalPages;

        var pagedOrders = filtered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var viewModel = new OrderListViewModel
        {
            Orders = pagedOrders,
            Search = search,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return View(viewModel);
    }

    // GET /Orders/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // GET /Orders/Create
    public IActionResult Create() => View(new Order());

    // POST /Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        if (!ModelState.IsValid) return View(order);
        await _orderService.CreateAsync(order);
        return RedirectToAction(nameof(Index));
    }

    // GET /Orders/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // POST /Orders/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Order order)
    {
        if (!ModelState.IsValid) return View(order);
        var updated = await _orderService.UpdateAsync(id, order);
        if (updated is null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    // GET /Orders/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // POST /Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _orderService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
