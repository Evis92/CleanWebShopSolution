using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Customer;

namespace WebShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ICustomerService _customerService;

	public CustomerController(ICustomerService customerService)
	{
		_customerService = customerService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
	{
		try
		{
			var allCustomers = await _customerService.GetAll();
			if (allCustomers == null || !allCustomers.Any())
			{
				return NotFound("No customers found");
			}

			return Ok(allCustomers);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"An error occured {ex.Message}");
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Customer>> GetCustomerById(int id)
	{
		try
		{
			var customer = await _customerService.GetById(id);
			if (customer == null)
			{
				return NotFound("No customers found");
			}

			return Ok(customer);
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Something went wrong: {e.Message}");
		}
	}

	[HttpPost]
	public async Task<ActionResult> AddCustomer(Customer customer)
	{
		if (customer == null)
			return BadRequest("Customer is invalid");
		try
		{
			await _customerService.Add(customer);
			return Ok("Customer was added");
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal server error: {e.Message}");
		}
	}

	[HttpPut]
	public async Task<ActionResult> UpdateCustomer(Customer customer)
	{
		if (customer == null)
			return BadRequest($"Customer is invalid");

		try
		{
			await _customerService.Update(customer);
			return Ok();
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal server errror {e.Message}");
		}
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteCustomer(int id)
	{
		if (id == null)
			return BadRequest($"Invalid id submitted");
		try
		{
			await _customerService.Delete(id);
			return Ok();
		}
		catch (Exception e)
		{
			return StatusCode(500, $"Internal Server error");
		}
	}
}