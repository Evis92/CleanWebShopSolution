using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Instrastructure.Data;
using WebShop.Instrastructure.Repositories;


namespace WebShopTests.RepositoryTests;

public class CustomerRepositoryTest
{
	// INTEGRATIONSTESTER
	[Fact]
	public async Task Add_CustomerRepository_AddsCustomerToDatabase()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "AddCustomerTestDatabase")
			.Options;

		using var context = new WebShopDbContext(options);
		var customerRepository = new CustomerRepository(context);
		var newCustomer = new Customer { Id = 1, Name = "Test User", Email = "test@example.com", Address = "testAdress 2"};

		// Act
		await customerRepository.Add(newCustomer);
		context.SaveChanges(); // Simulera SaveChanges som normalt sker i service-lager

		// Assert
		var customerInDb = context.Customers.Find(1);
		Assert.NotNull(customerInDb);
		Assert.Equal("Test User", customerInDb.Name);
	}

	[Fact]
	public async Task GetCustomerByEmail_CustomerRepository_ReturnsCorrectCustomer()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetCustomerByEmailTestDatabase")
			.Options;

		using var context = new WebShopDbContext(options);
		var customerRepository = new CustomerRepository(context);

		var existingCustomer = new Customer { Id = 1, Name = "Test User", Email = "test@example.com", Address = "testAdress 2"};
		customerRepository.Add(existingCustomer);
		context.SaveChanges();
		
		// Act
		var result = await customerRepository.GetCustomerByEmail("test@example.com");

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Test User", result.Name);
		Assert.Equal("test@example.com", result.Email);
	}

	[Fact]
	public async Task Delete_CustomerRepository_RemovesCustomerFromDatabase()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "DeleteCustomerTestDatabase")
			.Options;

		using var context = new WebShopDbContext(options);
		var customerRepository = new CustomerRepository(context);

		var existingCustomer = new Customer { Id = 1, Name = "Test User", Email = "test@example.com", Address = "testAdress 2" };
		context.Customers.Add(existingCustomer);
		context.SaveChanges();

		// Act
		await customerRepository.Delete(1);
		context.SaveChanges();

		// Assert
		var customerInDb = context.Customers.Find(1);
		Assert.Null(customerInDb);
	}

	[Fact]
	public async Task Update_CustomerRepository_UpdatesCustomerDetails()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateCustomerTestDatabase")
			.Options;

		using var context = new WebShopDbContext(options);
		var customerRepository = new CustomerRepository(context);

		var existingCustomer = new Customer { Id = 1, Name = "Test User", Email = "test@example.com", Address = "testAdress 2" };
		context.Customers.Add(existingCustomer);
		context.SaveChanges();

		// Act
		existingCustomer.Name = "Updated User";
		await customerRepository.Update(existingCustomer);
		context.SaveChanges();

		// Assert
		var customerInDb = context.Customers.Find(1);
		Assert.NotNull(customerInDb);
		Assert.Equal("Updated User", customerInDb.Name);
	}

	[Fact]
	public async Task GetAll_CustomerRepository_ReturnsCorrectList()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAllCustomerDatabase")
			.Options;

		using var context = new WebShopDbContext(options);
		var customerRepository = new CustomerRepository(context);

		var ListOfCustomers = new List<Customer>
		{
			new Customer(){ Id = 1, Name = "customer1", Email = "email1", Address = "Adress 1"},
			new Customer(){ Id = 2, Name = "customer2", Email = "email2", Address = "Adress 2"},
			new Customer(){ Id = 3, Name = "customer3", Email = "email3", Address = "Adress 3"},
		};

		foreach (var customer in ListOfCustomers)
		{
			customerRepository.Add(customer);
		}
		context.SaveChanges();
		
		// Act
		var result = await customerRepository.GetAll();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(result, ListOfCustomers);
		Assert.Equal(3, result.Count());
		Assert.Contains(result, c => c.Name == "customer1");
	}

	[Fact]
	public async Task GetById_CustomerRepository_ReturnsCorrectCustomer()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetByIdDataBase")
			.Options;
		using var context = new WebShopDbContext(options);
		var customerRepository = new CustomerRepository(context);

		var ListOfCustomers = new List<Customer>
		{
			new Customer(){ Id = 1, Name = "customer1", Email = "email1", Address = "Adress 1"},
			new Customer(){ Id = 2, Name = "customer2", Email = "email2", Address = "Adress 2"},
			new Customer(){ Id = 3, Name = "customer3", Email = "email3", Address = "Adress 3"},
		};

		foreach (var customer in ListOfCustomers)
		{
			customerRepository.Add(customer);
		}
		context.SaveChanges();

		// Act
		var result = await customerRepository.GetById(2);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("customer2", result.Name);
		Assert.Equal("Adress 2", result.Address);
	}
}