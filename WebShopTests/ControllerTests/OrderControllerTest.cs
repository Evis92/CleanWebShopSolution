using FakeItEasy;
using WebShop.Controllers;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Order;


namespace WebShopTests.ControllerTests;

public class OrderControllerTest
{
	private readonly IOrderService _fakeOrderService;
	private readonly OrderController _controller;

	public OrderControllerTest()
	{
		_fakeOrderService = A.Fake<IOrderService>();
		_controller = new OrderController(_fakeOrderService);
	}

	[Fact]
	public async Task GetAll_OrderService_HappensOnce()
	{
		// Arrange


		// Act
		var result = await _controller.GetOrders();

		// Assert
		A.CallTo(() => _fakeOrderService.GetAll()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetById_OrderService_HappensOnce()
	{
		// Arrange
		var fakeId = 1;

		// Act
		var result = await _controller.GetOrder(fakeId);

		// Assert
		A.CallTo(() => _fakeOrderService.GetById(fakeId)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Add_OrderService_HappensOnce()
	{
		// Arrange
		var fakeOrder = A.Fake<Order>();

		// Act
		var result = await _controller.AddOrder(fakeOrder);

		// Assert
		A.CallTo(() => _fakeOrderService.AddOrder_VerifyProduct(fakeOrder)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Update_OrderService_HappensOnce()
	{
		// Arrange
		var fakeOrder = A.Fake<Order>();

		// Act
		var result = await _controller.UpdateOrder(fakeOrder);

		// Assert
		A.CallTo(() => _fakeOrderService.Update(fakeOrder)).MustHaveHappenedOnceExactly();

	}

	[Fact]
	public async Task Delete_OrderService_HappensOnce()
	{
		// Arrange
		var fakeId = 1;

		// Act
		var result = _controller.DeleteOrder(fakeId);

		// Assert
		A.CallTo(() => _fakeOrderService.Delete(fakeId)).MustHaveHappenedOnceExactly();
	}
}