using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Product;
using WebShop.Instrastructure.Data;

namespace WebShop.Instrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
	public ProductRepository(WebShopDbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Product>> GetProductByName(string name)
	{
		return await _dbSet.Where(p => p.Name == name).ToListAsync();
	}
}