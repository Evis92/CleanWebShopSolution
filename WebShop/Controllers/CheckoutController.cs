using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Payment;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;

namespace WebShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
	private readonly CheckoutService _checkoutService;

	public CheckoutController(CheckoutService checkoutService)
	{
		_checkoutService = new CheckoutService();
	}

	[HttpPost("process")]
	public async Task<ActionResult> ProcessPayment(decimal amount, string paymentMethod)
	{
		if (string.IsNullOrWhiteSpace(paymentMethod))
			return BadRequest("Payment method cannot be null or empty");
		try
		{
			await _checkoutService.ProcessPayment(amount, paymentMethod);
			return Ok($"Payment of {amount:C} using {paymentMethod} was processed");
		}
		catch (Exception e)
		{
			return StatusCode(500, $"An unexpected error occurred while processing the payment { e.Message}");
		}
	}
}