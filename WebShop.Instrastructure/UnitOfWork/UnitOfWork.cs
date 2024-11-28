using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Customer;
using WebShop.Core.Interfaces.Order;
using WebShop.Core.Interfaces.Product;
using WebShop.Instrastructure.Data;
using WebShop.Instrastructure.Repositories;
using WebShop.Notifications;

namespace WebShop.Instrastructure.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly WebShopDbContext _dbContext;
		private readonly ProductSubject _productSubject;
		public IProductRepository Products { get; }
		public IOrderRepository Orders { get; }
		public ICustomerRepository Customer { get; }

		public UnitOfWork(WebShopDbContext dbContext, ProductSubject productSubject, IProductRepository productRepository, IOrderRepository ordersRepository, ICustomerRepository customerRepository)
		{
			_dbContext = dbContext;
			_productSubject = productSubject;
			Products = productRepository;
			Orders = ordersRepository;
			Customer = customerRepository;
			_productSubject = productSubject ?? new ProductSubject();

			_productSubject.Attach(new EmailNotification());
			_productSubject.Attach(new SmsNotification());
		}


		public IRepository<T> Repository<T>() where T : class
		{
			return new Repository<T>(_dbContext);
		}

		public async Task NotifyProductAdded(Product product)
		{
			_productSubject.Notify(product);
		}

		public async Task Complete()
		{
			await _dbContext.SaveChangesAsync();

		}

		public void Dispose()
		{
			_dbContext.Dispose();

		}
	}
}
