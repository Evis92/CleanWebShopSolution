using FakeItEasy;
using WebShop.Application.Services;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Product;

namespace WebShopTests.UnitOfWorkTests;

public class ProductUnitOfWorkTests
{
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly IProductRepository _fakeProductRepository;

	public ProductUnitOfWorkTests()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_fakeProductRepository = A.Fake<IProductRepository>();

		A.CallTo(() => _fakeUnitOfWork.Repository<Product>()).Returns(_fakeProductRepository);
	}

	[Fact]
	public async Task AddProduct_UnitOfWork_ShouldCallComplete()
	{

		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var product = A.Fake<Product>();

		// Act
		await productService.Add(product);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task AddProduct_WhenRepositoryThrowsException_ShouldNotCallComplete()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var product = A.Fake<Product>();

		// Simulera att repository kastar ett undantag
		A.CallTo(() => _fakeProductRepository.Add(product))
			.Throws(new Exception("Test exception"));

		// Act & Assert
		await Assert.ThrowsAsync<Exception>(() => productService.Add(product));

		// Verifiera att Complete INTE anropades
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustNotHaveHappened();
	}

	[Fact]
	public async Task AddProduct_ShouldAddAndCompleteInOrder()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var product = new Product { Id = 1, Name = "Test Product" };

		// Act
		await productService.Add(product);

		// Assert
		A.CallTo(() => _fakeProductRepository.Add(product)).MustHaveHappenedOnceExactly()
			.Then(A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly());
	}

	[Fact]
	public async Task DeleteProduct_UnitOfWork_ShouldCallComplete()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var id = 1;

		// Act
		await productService.Delete(id);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task UpdateProduct_UnitOfWork_ShouldCallComplete()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var product = A.Fake<Product>();

		// Act
		await productService.Update(product);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Complete()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetAllProducts_UnitOfWorks_ShouldNotCallComplete()
	{
		// Arrange

		// Act
		var result = _fakeProductRepository.GetAll();

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Products.GetAll()).MustNotHaveHappened();
	}
}