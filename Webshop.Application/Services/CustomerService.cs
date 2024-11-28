using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Customer;
using WebShop.Core.Interfaces;

namespace WebShop.Application.Services;

public class CustomerService : GenericService<Customer>, ICustomerService
{
	private readonly IUnitOfWork _unitOfWork;

	public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	//public Customer GetCustomerByEmail(string email)
	//{
	//	throw new NotImplementedException();
	//}
}