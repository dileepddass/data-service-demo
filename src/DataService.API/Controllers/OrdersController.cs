using DataService.API.Models;
using DataService.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;

    public OrdersController(IOrderRepository repository)
    {
        _repository = repository;
    }

    // GET api/orders
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    // GET api/orders/5
    [HttpGet("{id:int}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = _repository.GetById(id);
        if (order is null) return NotFound();
        return Ok(order);
    }

    // POST api/orders
    [HttpPost]
    public ActionResult<Order> Create([FromBody] Order order)
    {
        var created = _repository.Create(order);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/orders/5
    [HttpPut("{id:int}")]
    public ActionResult<Order> Update(int id, [FromBody] Order order)
    {
        var updated = _repository.Update(id, order);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    // DELETE api/orders/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
