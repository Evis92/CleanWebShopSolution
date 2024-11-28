using FakeItEasy;
using WebShop.Application.Services;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Order;


namespace WebShopTests.ServiceTests;

public class OrderServiceTest
{
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly IOrderRepository _fakeOrderRepository;

	public OrderServiceTest()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_fakeOrderRepository = A.Fake<IOrderRepository>();

		A.CallTo(() => _fakeUnitOfWork.Repository<Order>()).Returns(_fakeOrderRepository);
		A.CallTo(() => _fakeUnitOfWork.Orders).Returns(_fakeOrderRepository);
	}

	[Fact]
	public async Task GetAll_OrderService_HappensOnce()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		// Act
		var result = await orderService.GetAll();

		// Assert
		A.CallTo(() => _fakeOrderRepository.GetAll()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetAll_OrderService_ReturnsList()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var fakeOrderList = A.CollectionOfDummy<Order>(5);

		A.CallTo(() => _fakeOrderRepository.GetAll()).Returns(fakeOrderList);

		// Act
		var result = await orderService.GetAll();

		// Assert
		Assert.Equal(fakeOrderList, result);
		Assert.NotNull(result);

	}

	[Fact]
	public async Task Add_OrderService_HappensOnce()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var fakeOrder = new Order
		{
			Id = 1,
			Products = new List<Product>
			{
				new Product { Id = 101, Name = "Product 1" },
				new Product { Id = 102, Name = "Product 2" }
			},
			CustomerId = 2
		};

		// Act
		await orderService.AddOrder_VerifyProduct(fakeOrder);

		// Assert
		A.CallTo(() => _fakeOrderRepository.Add(fakeOrder)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Add_OrderService_AddsToList()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var fakeOrder = A.Dummy<Order>();
		var fakeOrderList = A.CollectionOfDummy<Order>(5);
		A.CallTo(() => _fakeOrderRepository.Add(fakeOrder))
			.Invokes((Order o) => fakeOrderList.Add(o));

		// Act
		await orderService.Add(fakeOrder);

		// Assert
		Assert.Equal(6, fakeOrderList.Count);
	}

	[Fact]
	public async Task Update_OrderService_HappensOnce()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var fakeOrder = A.Dummy<Order>();

		// Act
		await orderService.Update(fakeOrder);

		// Assert
		A.CallTo(() => _fakeOrderRepository.Update(fakeOrder)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Delete_OrderService_HappensOnce()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var orderId = 1;

		// Act
		await orderService.Delete(orderId);
		// Assert
		A.CallTo(() => _fakeOrderRepository.Delete(orderId)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetById_OrderService_HappensOnce()
	{
		// Arrange
		var orderService = new OrderService(_fakeUnitOfWork);
		var orderId = 1;

		// Act
		await orderService.GetById(orderId);
		// Assert
		A.CallTo(() => _fakeOrderRepository.GetById(orderId)).MustHaveHappenedOnceExactly();
	}
}