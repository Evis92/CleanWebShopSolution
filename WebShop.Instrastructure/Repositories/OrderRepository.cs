using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Order;
using WebShop.Instrastructure.Data;

namespace WebShop.Instrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{

	public OrderRepository(WebShopDbContext context) : base(context)
	{
	}

	public IEnumerable<Order> GetCustomerOrders(int customerId)
	{
		return _dbSet.Where(order => order.CustomerId == customerId);
	}

	public override async Task<IEnumerable<Order>> GetAll()
	{
		return await _dbSet
			.Include(o => o.Products)
			.ToListAsync();
	}
}