using WebShop.Core.Entities;
using WebShop.Core.Interfaces;

namespace WebShop.Notifications;

public class SmsNotification : INotificationObserver
{
	public void Update(Product product)
	{
		Console.WriteLine($"Sms Notification: New product added - {product.Name}");
	}
}