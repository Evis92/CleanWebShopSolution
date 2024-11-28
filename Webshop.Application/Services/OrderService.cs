using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Order;
using WebShop.Core.Interfaces;

namespace WebShop.Application.Services;

public class OrderService : GenericService<Order>, IOrderService
{
	private readonly IUnitOfWork _unitOfWork;

	public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task AddOrder_VerifyProduct(Order order)
	{
		var validatedProducts = new List<Product>();

		foreach (var product in order.Products)
		{
			var fetchedProduct = await _unitOfWork.Products.GetById(product.Id);
			if (fetchedProduct == null)
			{
				throw new ArgumentException($"Product with ID {product.Id} does not exist.");
			}

			validatedProducts.Add(fetchedProduct);
		}

		order.Products.Clear();
		foreach (var validatedProduct in validatedProducts)
		{
			order.Products.Add(validatedProduct);
		}

		await Add(order);
	}

	public override Task<IEnumerable<Order>> GetAll()
	{
		return _unitOfWork.Orders.GetAll();
	}
}