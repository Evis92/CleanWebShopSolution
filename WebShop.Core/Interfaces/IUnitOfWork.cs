using WebShop.Core.Interfaces.Customer;
using WebShop.Core.Interfaces.Order;
using WebShop.Core.Interfaces.Product;

namespace WebShop.Core.Interfaces
{
	
	public interface IUnitOfWork : IDisposable
	{
		IProductRepository Products { get; }
		IOrderRepository Orders { get; }
		ICustomerRepository Customer { get; }
		IRepository<T> Repository<T>() where T : class;

		Task NotifyProductAdded(Entities.Product product);

		Task Complete();
	}
}

