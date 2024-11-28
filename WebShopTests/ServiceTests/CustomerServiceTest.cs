using FakeItEasy;
using WebShop.Application.Services;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Customer;


namespace WebShopTests.ServiceTests;

public class CustomerServiceTest
{
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly ICustomerRepository _fakeICustomerRepository;

	public CustomerServiceTest()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_fakeICustomerRepository = A.Fake<ICustomerRepository>();

		A.CallTo(() => _fakeUnitOfWork.Repository<Customer>()).Returns(_fakeICustomerRepository);
	}

	[Fact]
	public async Task GetAll_CustomerService_HappensOnce()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);

		// Act
		var result = await customerService.GetAll();

		// Assert
		A.CallTo(() => _fakeICustomerRepository.GetAll()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetAll_CustomerService_ReturnsList()
	{
		var customerService = new CustomerService(_fakeUnitOfWork);
		var mockCustomerList = A.CollectionOfDummy<Customer>(4);

		A.CallTo(() => _fakeICustomerRepository.GetAll()).Returns(mockCustomerList);

		// Act
		var result = await customerService.GetAll();

		// Assert
		Assert.Equal(result, mockCustomerList);
	}

	[Fact]
	public async Task Add_CustomerService_HappensOnce()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var mockCustomer = A.Dummy<Customer>();

		// Act
		await customerService.Add(mockCustomer);

		// Assert
		A.CallTo(() => _fakeICustomerRepository.Add(mockCustomer)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetById_CustomerService_HappensOnce()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var customerId = 1;

		// Act
		await customerService.GetById(customerId);

		// Assert
		A.CallTo(() => _fakeICustomerRepository.GetById(customerId)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Update_CustomerService_HappensOnce()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var fakeCustomer = A.Dummy<Customer>();

		// Act
		await customerService.Update(fakeCustomer);

		// Assert
		A.CallTo(() => _fakeICustomerRepository.Update(fakeCustomer)).MustHaveHappened();
	}

	[Fact]
	public async Task Delete_CustomerService_HappensOnce()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var customerId = 1;

		// Act
		await customerService.Delete(customerId);

		// Assert
		A.CallTo(() => _fakeICustomerRepository.Delete(customerId)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Delete_CustomerService_DeletesCorrectly()
	{
		// Arrange
		var customerService = new CustomerService(_fakeUnitOfWork);
		var fakeCustomerList = A.CollectionOfDummy<Customer>(5);
		var customerId = fakeCustomerList[1].Id;

		A.CallTo(() => _fakeICustomerRepository.Delete(customerId))
			.Invokes((int id) =>
			{
				var customerToRemove = fakeCustomerList.FirstOrDefault(c => c.Id == id);
				if (customerToRemove != null)
				{
					fakeCustomerList.Remove(customerToRemove);
				}
			});

		// Act
		await customerService.Delete(customerId);

		// Assert
		Assert.Equal(4, fakeCustomerList.Count);
		//Assert.DoesNotContain(fakeCustomerList, c => c.Id == customerId);
	}
}