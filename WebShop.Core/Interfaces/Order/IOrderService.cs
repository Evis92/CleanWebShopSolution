namespace WebShop.Core.Interfaces.Order;

public interface IOrderService : IGenericService<Entities.Order>
{

	Task AddOrder_VerifyProduct(Entities.Order order);
}