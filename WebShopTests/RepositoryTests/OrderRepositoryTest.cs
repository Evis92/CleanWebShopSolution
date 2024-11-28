
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Instrastructure.Data;
using WebShop.Instrastructure.Repositories;


namespace WebShopTests.RepositoryTests;

public class OrderRepositoryTest
{

	[Fact]
	public async Task GetAll_OrderRepository_ReturnsCorrectList()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAllOrdersDatabase")
			.Options;
		using var context = new WebShopDbContext(option);
		var orderRepository = new OrderRepository(context);

		var orderList = new List<Order>
		{
			new Order() { Id = 1, Products = new List<Product>(), CustomerId = 3 },
			new Order() { Id = 2, Products = new List<Product>(), CustomerId = 4 },
			new Order() { Id = 3, Products = new List<Product>(), CustomerId = 3 }
		};

		foreach (var order in orderList)
		{
			orderRepository.Add(order);
		}

		context.SaveChanges();

		// Act
		var result = await orderRepository.GetAll();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Count());
		Assert.Equal(orderList, result);
		Assert.Contains(result, o => o.CustomerId == 4);
	}

	[Fact]
	public async Task GetById_OrderRepository_ReturnsCorrectOrder()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "OrderDataBase")
			.Options;

		var context = new WebShopDbContext(option);
		var orderRepository = new OrderRepository(context);

		var orderList = new List<Order>
		{
			new Order() { Id = 1, Products = new List<Product>(), CustomerId = 3 },
			new Order() { Id = 2, Products = new List<Product>(), CustomerId = 4 },
			new Order() { Id = 3, Products = new List<Product>(), CustomerId = 3 }
		};

		foreach (var order in orderList)
		{
			await orderRepository.Add(order);
		}

		context.SaveChanges();

		// Act
		var result = await orderRepository.GetById(2);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(4, result.CustomerId);
	}

	[Fact]
	public async Task Add_OrderRepository_AddsCorrectly()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "AddOrderDatabase")
			.Options;
		var context = new WebShopDbContext(option);
		var orderRepository = new OrderRepository(context);

		var newOrder = new Order() { Id = 1, CustomerId = 2, Products = new List<Product>() };

		// Act
		await orderRepository.Add(newOrder);
		context.SaveChanges();

		// Assert
		var orderInDb = context.Orders.Find(1);
		Assert.NotNull(orderInDb);
		Assert.Equal(orderInDb, newOrder);

	}

	[Fact]
	public async Task Update_OrderRepository_UpdatesCorrectly()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateOrderDatabase")
			.Options;
		var context = new WebShopDbContext(option);
		var orderRepository = new OrderRepository(context);

		var originalOrder = new Order() { Id = 1, Products = new List<Product>(), CustomerId = 2 };
		await orderRepository.Add(originalOrder);
		context.SaveChanges();

		originalOrder.CustomerId = 3;


		// Act
		await orderRepository.Update(originalOrder);
		context.SaveChanges();

		// Assert
		var orderInDb = context.Orders.Find(1);
		var allOrders = await orderRepository.GetAll();
		Assert.NotNull(orderInDb);
		Assert.Equal(1, allOrders.Count());

	}

	[Fact]
	public async Task Delete_OrderRepository_DeletesCorrectOrder()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "OrderDatabase")
			.Options;
		var context = new WebShopDbContext(option);
		var orderRepository = new OrderRepository(context);
		var id = 1;
		var orderList = new List<Order>
		{
			new Order() { Id = 1, Products = new List<Product>(), CustomerId = 3 },
			new Order() { Id = 2, Products = new List<Product>(), CustomerId = 4 },
			new Order() { Id = 3, Products = new List<Product>(), CustomerId = 3 }
		};

		foreach (var order in orderList)
		{
			orderRepository.Add(order);
		}

		// Act
		await orderRepository.Delete(id);
		context.SaveChanges();

		// Assert
		var cutomerInDb = context.Orders.Find(1);
		Assert.Null(cutomerInDb);
	}
}