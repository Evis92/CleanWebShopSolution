using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Product;
using WebShop.Core.Interfaces;

namespace WebShop.Application.Services;

public class ProductService : GenericService<Product>, IProductService
{
	private readonly IUnitOfWork _unitOfWork;

	public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Product>> GetProductByName(string name)
	{
		return await _unitOfWork.Products.GetProductByName(name);
	}

	public async Task AddProduct(Product product)
	{

		await Add(product);

		await _unitOfWork.NotifyProductAdded(product);
	}
}