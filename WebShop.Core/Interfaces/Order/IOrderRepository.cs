namespace WebShop.Core.Interfaces.Order;

public interface IOrderRepository : IRepository<Entities.Order>
{
	IEnumerable<Entities.Order> GetCustomerOrders(int customerId);
}