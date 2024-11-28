using FakeItEasy;
using WebShop.Application.Services;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Product;


namespace WebShopTests.ServiceTests;

public class ProductServiceTest
{
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly IProductRepository _fakeProductRepository;

	public ProductServiceTest()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_fakeProductRepository = A.Fake<IProductRepository>();

		A.CallTo(() => _fakeUnitOfWork.Repository<Product>()).Returns(_fakeProductRepository);
	}

	[Fact]
	public async Task GetAll_ProductService_GetsCalledOnce()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var expectedProductList = A.CollectionOfDummy<Product>(3);

		// Act
		var result = await productService.GetAll();

		// Assert
		A.CallTo(() => _fakeProductRepository.GetAll()).MustHaveHappenedOnceExactly();
		
	}

	[Fact]
	public async Task GetAll_ProductService_ReturnsListOfProducts()
	{
		// Arrange
		var dummyProductList = A.CollectionOfDummy<Product>(4);

		A.CallTo(() => _fakeProductRepository.GetAll()).Returns(dummyProductList);
		var productService = new ProductService(_fakeUnitOfWork);

		// Act
		var result = await productService.GetAll();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(dummyProductList, result);
	}

	[Fact]
	public async Task GetProductById_ProductService_HappensOnce()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var productId = 1;

		// Act
		await productService.GetById(productId);

		// Assert
		A.CallTo(() => _fakeProductRepository.GetById(productId)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetProductById_ProductService_ReturnsCorrect()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var dummyProduct = A.Dummy<Product>();
		var productId = 1;

		A.CallTo(() => _fakeProductRepository.GetById(productId))
			.Returns(dummyProduct);

		// Act
		var result = await productService.GetById(productId);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(dummyProduct, result);
	}

	[Fact]
	public async Task Add_ProductService_GetsCalledOnce()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		var productList = A.CollectionOfDummy<Product>(4);
		var product = A.Dummy<Product>();

		// Act
		await productService.Add(product);

		// Assert
		A.CallTo(() => _fakeProductRepository.Add(product)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Add_ProductService_AddsCorrectType()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		
		var productList = A.CollectionOfDummy<Product>(4);
		var product = A.Dummy<Product>();

		A.CallTo(() => _fakeProductRepository.Add(product))
			.Invokes((Product p) => productList.Add(p));

		// Act
		await productService.Add(product);


		// Assert
		Assert.Contains(product, productList);
		Assert.Equal(5, productList.Count);
	}

	[Fact]
	public async Task Delete_ProductService_GetsCalledOnce()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);
		
		var dummyProductList = A.CollectionOfDummy<Product>(4);
		var dummyProductId = 1;


		// Act
		await productService.Delete(dummyProductId);

		// Assert
		A.CallTo(() => _fakeProductRepository.Delete(dummyProductId)).MustHaveHappenedOnceExactly();
		Assert.DoesNotContain(dummyProductList, p => p.Id == dummyProductId);
	}

	[Fact]
	public async Task Update_ProductService_HappensOnce()
	{
		// Arrange
		var productService = new ProductService(_fakeUnitOfWork);

		var product = A.Dummy<Product>();

		// Act
		await productService.Update(product);

		// Assert
		A.CallTo(() => _fakeProductRepository.Update(product)).MustHaveHappenedOnceExactly();
	}



	//[Fact]
	//public async Task GetProductByName_ProductService_ReturnsCorrectProduct()
	//{
	//	// Arrange
	//	var productService = new ProductService(_fakeUnitOfWork);
	//	var dummyProductName = "Laptop";

	//	// Act
	//	var result = await productService.GetProductByName(dummyProductName);

	//	// Assert
	//	A.CallTo(() => _fakeProductRepository.GetProductByName(dummyProductName)).MustHaveHappenedOnceExactly();

	//}
}
