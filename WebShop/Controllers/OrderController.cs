using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Order;

namespace WebShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
	private readonly IOrderService _orderService;
	public OrderController(IOrderService orderService)
	{
		_orderService = orderService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
	{
		try
		{
			var orders = await _orderService.GetAll();
			if (orders == null)
			{
				return NotFound($"No orders found");
			}
			return Ok(orders);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal server error {e.Message}");
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Order>> GetOrder(int id)
	{
		if (id == null)
			return BadRequest($"Invalid id");
		try
		{
			var order = await _orderService.GetById(id);
			if (order == null)
			{
				return NotFound("No order found.");
			}
			return Ok(order);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Inrenal server error");
		}


	}

	[HttpPost]
	public async Task<ActionResult> AddOrder(Order order)
	{
		if (order == null)
			return BadRequest("Order is invalid");
		try
		{
			await _orderService.AddOrder_VerifyProduct(order);
			return Ok();
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal ORDER server error: {e.Message}");
		}
	}

	[HttpPut]
	public async Task<ActionResult> UpdateOrder(Order order)
	{
		if (order == null)
			return BadRequest($"Order is invalid");

		try
		{
			await _orderService.Update(order);
			return Ok();
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal server errror {e.Message}");
		}
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteOrder(int id)
	{
		if (id == null)
			return BadRequest($"Invalid id submitted");
		try
		{
			await _orderService.Delete(id);
			return Ok();
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal Server error");
		}
	}
}