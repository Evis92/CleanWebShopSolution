using WebShop.Core.Interfaces;

namespace WebShop.Application.Payment;

public class PaymentContext
{
	private IPaymentStrategy _paymentStrategy;

	public async Task<bool> ExecutePayment(decimal amount)
	{
		if (_paymentStrategy == null)
		{
			throw new InvalidOperationException("Payment strategy is not set.");
		}

		return await _paymentStrategy.Pay(amount);
	}

	public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
	{
		_paymentStrategy = paymentStrategy;
	}
}