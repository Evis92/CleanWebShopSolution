using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Instrastructure.Data;
using WebShop.Instrastructure.Repositories;


namespace WebShopTests.RepositoryTests;

public class ProductRepositoryTest
{
	[Fact]
	public void Add_ProductRepository_AddsCorrectProduct()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "testingProductRepoDatabase")
			.Options;

		using var context = new WebShopDbContext(option);
		var productRepository = new ProductRepository(context);

		var product = new Product() { Id = 1, Name = "productName", Price = 20 };

		// Act
		productRepository.Add(product);
		context.SaveChanges();

		// Assert
		var productInDb = context.Products.Find(product.Id);
		Assert.NotNull(productInDb);
		Assert.Equal("productName", productInDb.Name);
	}

	[Fact]
	public void Delete_ProductRepository_DeletesCorrectProduct()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "ProductDB")
			.Options;
		using var context = new WebShopDbContext(option);
		var productRepository = new ProductRepository(context);

		var productToDelete = 1;

		var productList = new List<Product>
		{
			new Product() { Id = 1, Name = "prodDelete", Price = 20 },
			new Product() { Id = 2, Name = "prod4", Price = 20 },
			new Product() { Id = 3, Name = "prod2", Price = 20 },
			new Product() { Id = 4, Name = "prod3", Price = 20 }
		};

		foreach (var product in productList)
		{
			productRepository.Add(product);
		}
		context.SaveChanges();

		// Act
		productRepository.Delete(productToDelete);
		context.SaveChanges();

		// Assert
		var customerInDb = context.Customers.Find(1);
		Assert.Null(customerInDb);
	}

	[Fact]
	public async Task GetAll_ProductRepository_GetCorrectList()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetAllProductsDb")
			.Options;

		using var context = new WebShopDbContext(option);
		var productRepository = new ProductRepository(context);

		var productList = new List<Product>
		{
			new Product() { Id = 1, Name = "prodDelete", Price = 20 },
			new Product() { Id = 2, Name = "prod4", Price = 20 },
			new Product() { Id = 3, Name = "prod2", Price = 20 },
			new Product() { Id = 4, Name = "prod3", Price = 20 }
		};

		foreach (var product in productList)
		{
			await productRepository.Add(product);
		}
		context.SaveChanges();

		// Act
		var allProducts = await productRepository.GetAll();

		// Assert
		Assert.NotNull(allProducts);
		Assert.Equal(productList, allProducts);
		Assert.Equal(4, allProducts.Count());
		Assert.Contains(allProducts, p => p.Name == "prod4");
	}

	[Fact]
	public void GetById_ProductRepository_GetCorrectProduct()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetProductByIdDb")
			.Options;

		using var context = new WebShopDbContext(option);
		var productRepository = new ProductRepository(context);

		var productList = new List<Product>
		{
			new Product() { Id = 1, Name = "prodDelete", Price = 20 },
			new Product() { Id = 2, Name = "prod4", Price = 20 },
			new Product() { Id = 3, Name = "prod2", Price = 20 },
			new Product() { Id = 4, Name = "prod3", Price = 20 }
		};

		foreach (var product in productList)
		{
			productRepository.Add(product);
		}
		context.SaveChanges();
		// Act

		// Assert
	}

	[Fact]
	public void Update_ProductRepository_UpdateCorrectProduct()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdateProductDb")
			.Options;

		using var context = new WebShopDbContext(option);
		var productRepository = new ProductRepository(context);

		var productList = new List<Product>
		{
			new Product() { Id = 1, Name = "prodDelete", Price = 20 },
			new Product() { Id = 2, Name = "prod4", Price = 20 },
			new Product() { Id = 3, Name = "prod2", Price = 20 },
			new Product() { Id = 4, Name = "prod3", Price = 20 }
		};

		foreach (var product in productList)
		{
			productRepository.Add(product);
		}
		context.SaveChanges();
		// Act

		// Assert
	}

	[Fact]
	public void GetByName_ProductRepository_GetCorrectProduct()
	{
		// Arrange
		var option = new DbContextOptionsBuilder<WebShopDbContext>()
			.UseInMemoryDatabase(databaseName: "GetProductByNameDb")
			.Options;

		using var context = new WebShopDbContext(option);
		var productRepository = new ProductRepository(context);

		var productList = new List<Product>
		{
			new Product() { Id = 1, Name = "prodDelete", Price = 20 },
			new Product() { Id = 2, Name = "prod4", Price = 20 },
			new Product() { Id = 3, Name = "prod2", Price = 20 },
			new Product() { Id = 4, Name = "prod3", Price = 20 }
		};

		foreach (var product in productList)
		{
			productRepository.Add(product);
		}
		context.SaveChanges();
		// Act

		// Assert
	}
}