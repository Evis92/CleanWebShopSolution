using WebShop.Core.Interfaces;

namespace WebShop.Application.Payment;

public class CheckoutService
{
	private readonly PaymentContext _paymentContext;

	public CheckoutService()
	{
		_paymentContext = new PaymentContext();
	}

	public async Task ProcessPayment(decimal amount, string paymentMethod)
	{
		// Välj strategi baserat på användarens val
		IPaymentStrategy paymentStrategy = paymentMethod switch
		{
			"Swish" => new SwishPayment(),
			"CreditCard" => new CreditCardPayment(),
			_ => throw new ArgumentException("Invalid payment method")
		};

		// Sätt strategin i PaymentContext
		_paymentContext.SetPaymentStrategy(paymentStrategy);

		// Kör betalningen
		bool success = await _paymentContext.ExecutePayment(amount);

		Console.WriteLine(success
			? "Payment processed successfully!"
			: "Payment failed.");
	}
}