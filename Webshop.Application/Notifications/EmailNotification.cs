using WebShop.Core.Entities;
using WebShop.Core.Interfaces;

namespace WebShop.Notifications
{
	// En konkret observatör som skickar e-postmeddelanden
	public class EmailNotification : INotificationObserver
	{
		public void Update(Product product)
		{
			Console.WriteLine($"Email Notification: New product added - {product.Name}");
		}
	}
}
