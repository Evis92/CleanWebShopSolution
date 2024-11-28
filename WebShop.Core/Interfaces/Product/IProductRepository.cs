namespace WebShop.Core.Interfaces.Product
{
	// Gränssnitt för produktrepositoryt enligt Repository Pattern
	public interface IProductRepository : IRepository<Entities.Product>
	{
		Task<IEnumerable<Entities.Product>>? GetProductByName(string name);
	}
}
