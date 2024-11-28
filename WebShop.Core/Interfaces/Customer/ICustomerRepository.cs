namespace WebShop.Core.Interfaces.Customer;


	public interface ICustomerRepository : IRepository<Entities.Customer>
	{
		Task<Entities.Customer> GetCustomerByEmail(string email);
	}
