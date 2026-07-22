using DataService.API.Models;
using DataService.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartRepository _repository;

    public CartsController(ICartRepository repository)
    {
        _repository = repository;
    }

    // GET api/carts?customerName=&status=
    [HttpGet]
    public ActionResult<IEnumerable<Cart>> GetAll([FromQuery] string? customerName, [FromQuery] string? status)
    {
        if (!string.IsNullOrWhiteSpace(customerName) || !string.IsNullOrWhiteSpace(status))
        {
            return Ok(_repository.Search(customerName, status));
        }

        return Ok(_repository.GetAll());
    }

    // GET api/carts/5
    [HttpGet("{id:int}")]
    public ActionResult<Cart> GetById(int id)
    {
        var cart = _repository.GetById(id);
        if (cart is null) return NotFound();
        return Ok(cart);
    }

    // POST api/carts
    [HttpPost]
    public ActionResult<Cart> Create([FromBody] Cart cart)
    {
        var created = _repository.Create(cart);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/carts/5
    [HttpPut("{id:int}")]
    public ActionResult<Cart> Update(int id, [FromBody] Cart cart)
    {
        var updated = _repository.Update(id, cart);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    // DELETE api/carts/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    // POST api/carts/5/lineitems
    [HttpPost("{cartId:int}/lineitems")]
    public ActionResult<CartLineItem> AddLineItem(int cartId, [FromBody] CartLineItem lineItem)
    {
        var created = _repository.AddLineItem(cartId, lineItem);
        if (created is null) return NotFound();
        return CreatedAtAction(nameof(GetById), new { id = cartId }, created);
    }

    // DELETE api/carts/5/lineitems/2
    [HttpDelete("{cartId:int}/lineitems/{lineItemId:int}")]
    public IActionResult DeleteLineItem(int cartId, int lineItemId)
    {
        var deleted = _repository.DeleteLineItem(cartId, lineItemId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
