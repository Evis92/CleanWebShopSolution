using WebShop.Core.Interfaces;

namespace WebShop.Application.Payment;

public class SwishPayment : IPaymentStrategy
{
	public async Task<bool> Pay(decimal amount)
	{
		Console.WriteLine($"Processing swish payment of {amount:C}.");
		return await Task.FromResult(true);
	}
}