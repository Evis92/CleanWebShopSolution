using FakeItEasy;
using WebShop.Application.Services;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Order;


namespace WebShopTests.UnitOfWorkTests;

public class OrderUnitOfWorkTest
{
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly IOrderRepository _fakeOrderRepository;

	public OrderUnitOfWorkTest()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_fakeOrderRepository = A.Fake<IOrderRepository>();

		A.CallTo(() => _fakeUnitOfWork.Repository<Order>()).Returns(_fakeOrderRepository);
	}

	[Fact]
	public async Task AddOrder_UnitOfWork_ShouldCallComplete()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var fakeOrder = A.Fake<Order>();

		// Act
		await orderService.Add(fakeOrder);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task UpdateOrder_UnitOfWork_ShouldCallComplete()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var fakeOrder = A.Fake<Order>();

		// Act
		await orderService.Update(fakeOrder);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}


	[Fact]
	public async Task DeleteOrder_UnitOfWork_ShouldCallComplete()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var id = 1;

		// Act
		await orderService.Delete(id);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetAllOrders_UnitOfWork_ShouldNotCallComplete()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);

		// Act
		var result = await orderService.GetAll();

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustNotHaveHappened();
	}

	[Fact]
	public async Task AddProduct_WhenRepositoryThrowsException_ShouldNotCallComplete()
	{
		// Arrange
		var productService = new OrderService(_fakeUnitOfWork);
		var fakeorder = A.Fake<Order>();

		// Simulera att repository kastar ett undantag
		A.CallTo(() => _fakeOrderRepository.Add(fakeorder))
			.Throws(new Exception("Test exception"));

		// Act & Assert
		await Assert.ThrowsAsync<Exception>(() => productService.Add(fakeorder));

		// Verifiera att Complete INTE anropades
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustNotHaveHappened();
	}
}