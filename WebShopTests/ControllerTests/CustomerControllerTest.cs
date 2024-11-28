using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop.Controllers;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Customer;


namespace WebShopTests.ControllerTests;

public class CustomerControllerTest
{
	private readonly ICustomerService _fakeCustomerService;
	private readonly CustomerController _controller;

	public CustomerControllerTest()
	{
		_fakeCustomerService = A.Fake<ICustomerService>();
		_controller = new CustomerController(_fakeCustomerService);
	}

	[Fact]
	public async Task GetAll_CustomerService_HappensOnce()
	{
		// Arrange
		

		// Act
		var result = await _controller.GetCustomers();

		// Assert
		A.CallTo(() => _fakeCustomerService.GetAll()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetAll_CustomerService_ReturnsCorrectList()
	{
		// Arrange


		// Act
		var result = await _controller.GetCustomers();

		// Assert
		A.CallTo(() => _fakeCustomerService.GetAll()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetById_CustomerService_HappensOnce()
	{
		// Arrange
		var id = 1;

		// Act
		var result = await _controller.GetCustomerById(id);

		// Assert
		A.CallTo(() => _fakeCustomerService.GetById(id)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetById_CustomerService_ReturnsCorrectCustomer()
	{
		// Arrange
		var id = 1;
		var fakeCustomer = A.Fake<Customer>();

		A.CallTo(() => _fakeCustomerService.GetById(id)).Returns(fakeCustomer);
		
		// Act
		var result = await _controller.GetCustomerById(id);

		// Assert
		//var actionResult = Assert.IsType<ActionResult<Customer>>(result);
		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var returnedCustomer = Assert.IsAssignableFrom<Customer>(okResult.Value);
		Assert.Equal(fakeCustomer, returnedCustomer);
	}

	[Fact]
	public async Task Add_CustomerService_HappensOnce()
	{
		// Arrange
		var customer = A.Fake<Customer>();

		// Act
		var result = await _controller.AddCustomer(customer);

		// Assert
		A.CallTo(() => _fakeCustomerService.Add(customer)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Update_CustomerService_HappensOnce()
	{
		// Arrange
		var customer = A.Fake<Customer>();

		// Act
		var result = await _controller.UpdateCustomer(customer);

		// Assert
		A.CallTo(() => _fakeCustomerService.Update(customer)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Delete_CustomerService_HappensOnce()
	{
		// Arrange
		var id = 1;

		// Act
		var result = await _controller.DeleteCustomer(id);

		// Assert
		A.CallTo(() => _fakeCustomerService.Delete(id)).MustHaveHappenedOnceExactly();
	}
}