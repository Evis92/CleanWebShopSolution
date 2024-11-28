namespace WebShop.Core.Interfaces;

public interface IPaymentStrategy
{
	Task<bool> Pay(decimal amount);
}