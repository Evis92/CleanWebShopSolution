using FakeItEasy;
using WebShop.Application.Services;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Customer;


namespace WebShopTests.UnitOfWorkTests;

public class CustomerUnitOfWorkTest
{
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly ICustomerRepository _fakeCustomerRepository;

	public CustomerUnitOfWorkTest()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_fakeCustomerRepository = A.Fake<ICustomerRepository>();

		A.CallTo(() => _fakeUnitOfWork.Repository<Customer>()).Returns(_fakeCustomerRepository);
	}

	[Fact]
	public async Task AddCustomer_UnitOfWork_ShouldComplete()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var fakeCustomer = A.Fake<Customer>();

		// Act
		await customerService.Add(fakeCustomer);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetCustomer_UnitOfWork_ShouldNotComplete()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var fakeCustomer = A.Fake<Customer>();
		

		// Act
		var result = await customerService.GetAll();

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustNotHaveHappened();
	}

	[Fact]
	public async Task UpdateCustomer_UnitOfWork_ShouldComplete()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var fakeCustomer = A.Fake<Customer>();

		// Act
		await customerService.Update(fakeCustomer);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task DeleteCustomer_UnitOfWork_ShouldComplete()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var id = 1;
		// Act
		await customerService.Delete(id);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetById_UnitOfWork_ShouldNotComplete()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var id = 1;

		// Act
		var result = await customerService.GetById(id);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustNotHaveHappened();
	}
}