using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Customer;
using WebShop.Instrastructure.Data;

namespace WebShop.Instrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
	public CustomerRepository(WebShopDbContext context) : base(context)
	{
	}

	public async Task<Customer> GetCustomerByEmail(string email)
	{
		var customer = _dbSet.FirstOrDefault(c => c.Email == email);
		return customer;
	}
}