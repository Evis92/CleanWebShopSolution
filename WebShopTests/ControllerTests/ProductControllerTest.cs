using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop.Controllers;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Product;
using Times = Moq.Times;

namespace WebShopTests.ControllerTests;

public class ProductControllerTest
{
	private readonly Mock<IProductService> _mockProductService;
	private readonly ProductController _controller;

	public ProductControllerTest()
	{
		_mockProductService = new Mock<IProductService>();

		_controller = new ProductController(_mockProductService.Object);
	}

	[Fact]
	public async Task GetAllProducts_ReturnsOkResult_WithAListOfProducts()
	{ 
		//Arrange
	   var dummyProductList = new List<Product>
	   {
			new Product { Id = 1, Name = "Banan", Price = 10 },
			new Product { Id = 2, Name = "Äpple", Price = 9 },
			new Product { Id = 3, Name = "Päron", Price = 11 }
	   };

		_mockProductService.Setup(s => s.GetAll()).ReturnsAsync(dummyProductList);

		// Act
		var result = await _controller.GetProducts();

		// Assert
		var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
		var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
		var products = Assert.IsType<List<Product>>(okResult.Value);
		Assert.Equal(dummyProductList, products);
		Assert.Equal(dummyProductList.Count, products.Count);
	}

	[Fact]
	public async Task GetProductById_ProductExist_ReturnsOkResult()
	{
		// Arrange
		var productId = 1;
		var dummyProduct = new Product { Id = 1, Name = "Banana", Price = 10 };
		_mockProductService.Setup(s => s.GetById(productId)).ReturnsAsync(dummyProduct);

		// Act
		var result = await _controller.GetProductById(productId);

		// Assert
		// Kontrollera att resultatet är av typen OkObjectResult
		var actionResult = Assert.IsType<ActionResult<Product>>(result); // Kontrollera ActionResult
		var okResult = Assert.IsType<OkObjectResult>(actionResult.Result); // Extrahera och validera OkObjectResult

		// Kontrollera att OkObjectResult innehåller rätt produkt
		var actualProduct = Assert.IsType<Product>(okResult.Value);
		Assert.Equal(dummyProduct, actualProduct); // Kontrollera att produkten matchar
	}

	[Fact]
	public async Task GetProductById_HappensOnce()
	{
		// Arrange
		var productId = 1;
		var dummyProduct = new Product { Id = 1, Name = "Banana", Price = 10 };
		_mockProductService.Setup(s => s.GetById(productId)).ReturnsAsync(dummyProduct);

		// Act
		var result = await _controller.GetProductById(productId);

		// Assert
		_mockProductService.Verify(m => m.GetById(productId), Times.Once);
	}

	[Fact]
	public async Task AddProduct_HappensOnce()
	{
		// Arrange
		var dummyProduct = new Product { Id = 1, Name = "Tröja", Price = 10 };
		_mockProductService.Setup(s => s.Add(It.IsAny<Product>()));

		// Act
		var result = await _controller.AddProduct(dummyProduct);

		// Assert
		_mockProductService.Verify(s => s.AddProduct(It.Is<Product>(p => p == dummyProduct)), Times.Once);
	}

	[Fact]
	public async Task AddProduct_ReturnsOkResult()
	{
		// Arrange
		var dummyProduct = new Product { Id = 1, Name = "Tröja", Price = 10 };
		_mockProductService.Setup(s => s.AddProduct(It.IsAny<Product>()));

		// Act
		var result = await _controller.AddProduct(dummyProduct);

		// Assert
		var createdResult = Assert.IsType<CreatedAtActionResult>(result);
		Assert.Equal("GetProductById", createdResult.ActionName); // Kontrollera att det pekar på rätt action
		Assert.Equal(dummyProduct.Id, ((Product)createdResult.Value).Id);
	}

	[Fact]
	public async Task DeleteProduct_HappensOnce()
	{
		// Arrange
		var dummyId = 1;
		_mockProductService.Setup(s => s.Delete(It.IsAny<int>()));

		// Act
		var result = await _controller.DeleteProduct(dummyId);

		// Assert
		_mockProductService.Verify(s => s.Delete(It.Is<int>(id => id == dummyId)), Times.Once);
	}

	[Fact]
	public async Task DeleteProduct_ReturnsOkResult()
	{
		// Arrange
		var dummyId = 1;
		_mockProductService.Setup(s => s.Delete(It.IsAny<int>()))
			.Returns(Task.CompletedTask);

		// Act
		var result = await _controller.DeleteProduct(dummyId);

		// Assert
		var deleteResult = Assert.IsType<OkObjectResult>(result);
		Assert.NotNull(deleteResult.Value);
	}

	[Fact]
	public async Task UpdateProduct_HappensOnce()
	{
		// Arrange
		var dummyProduct = new Product { Id = 1, Name = "Tröja", Price = 10 };
		_mockProductService.Setup(s => s.Update(It.IsAny<Product>()))
			.Returns(Task.CompletedTask);

		// Act
		var result = await _controller.UpdateProduct(dummyProduct);

		// Assert
		_mockProductService.Verify(s => s.Update(It.Is<Product>(p => p == dummyProduct)));
	}

	[Fact]
	public async Task UpdateProduct_ResturnsOkResult()
	{
		// Arrange
		var dummyProduct = new Product { Id = 1, Name = "Tröja", Price = 10 };
		_mockProductService.Setup(s => s.Update(It.IsAny<Product>()))
			.Returns(Task.CompletedTask);

		// Act
		var result = await _controller.UpdateProduct(dummyProduct);

		// Assert
		var updateResult = Assert.IsType<OkObjectResult>(result);
		Assert.NotNull(updateResult.Value);
	}

	[Fact]
	public async Task GetProductByName_ResturnsOkResult()
	{
		// Arrange
		var dummyProductName = "Banan";
		
		var dummyProductList = new List<Product>
		{
			new Product { Id = 1, Name = "Banan", Price = 10 },
			new Product { Id = 2, Name = "Banan", Price = 9 },
			new Product { Id = 3, Name = "Banan", Price = 11 }
		};

		_mockProductService.Setup(s => s.GetProductByName(It.IsAny<string>()))
			.ReturnsAsync(dummyProductList);
		// Act
		var result = await _controller.GetProductByName(dummyProductName);

		// Assert
		_mockProductService.Verify(s => s.GetProductByName(It.Is<string>(p => p == dummyProductName)));
	}
}
