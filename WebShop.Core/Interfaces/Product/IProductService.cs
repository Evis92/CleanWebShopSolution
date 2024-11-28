namespace WebShop.Core.Interfaces.Product;

public interface IProductService : IGenericService<Entities.Product>
{
	Task<IEnumerable<Entities.Product>> GetProductByName(string name);

	Task AddProduct(Entities.Product product);
}