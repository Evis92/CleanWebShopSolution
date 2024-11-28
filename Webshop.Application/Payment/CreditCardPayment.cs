using WebShop.Core.Interfaces;

namespace WebShop.Application.Payment;

public class CreditCardPayment : IPaymentStrategy
{
	public async Task<bool> Pay(decimal amount)
	{
		Console.WriteLine($"Processing creditcard payment of {amount:C}");
		return await Task.FromResult(true);
	}
}