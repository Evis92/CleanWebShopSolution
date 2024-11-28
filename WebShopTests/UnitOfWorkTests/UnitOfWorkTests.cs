using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Customer;
using WebShop.Core.Interfaces.Order;
using WebShop.Core.Interfaces.Product;
using WebShop.Instrastructure.Data;
using WebShop.Instrastructure.UnitOfWork;
using WebShop.Notifications;
using Times = Moq.Times;

namespace WebShopTests.UnitOfWorkTests
{
	public class UnitOfWorkTests
	{
		// FakeItEasy
		[Fact]
		public async Task NotifyProductAdded_CallsObserverUpdate()
		{
			// Arrange
			var fakeProduct = A.Fake<Product>();
			var fakeObserver = A.Fake<INotificationObserver>();

			var fakeProductRepo = A.Fake<IProductRepository>();
			var fakeOrderRepo = A.Fake<IOrderRepository>();
			var fakeCustomerRepo = A.Fake<ICustomerRepository>();

			var productSubject = new ProductSubject();
			productSubject.Attach(fakeObserver);
			var unitOfWork = new UnitOfWork(dbContext: null, productSubject, fakeProductRepo, fakeOrderRepo, fakeCustomerRepo);

			// Act
			await unitOfWork.NotifyProductAdded(fakeProduct);
			// Assert
			A.CallTo(() => fakeObserver.Update(fakeProduct)).MustHaveHappenedOnceExactly();
		}


		// INTEGRATIONSTEST
		[Fact]
		public async Task Dispose_ShouldCallDisposeOnDbContext()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<WebShopDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			var fakeDbContext = A.Fake<WebShopDbContext>(o =>
				o.WithArgumentsForConstructor(() => new WebShopDbContext(options)));

			var unitOfWork = new UnitOfWork(
				fakeDbContext,
				A.Fake<ProductSubject>(),
				A.Fake<IProductRepository>(),
				A.Fake<IOrderRepository>(),
				A.Fake<ICustomerRepository>()

			);

			// Act
			unitOfWork.Dispose();

			// Assert
			A.CallTo(() => fakeDbContext.Dispose()).MustHaveHappenedOnceExactly();
		}

		// MOQ LÖSNING
		[Fact]
		public async Task Dispose_ShouldCallDisposeOnDbContext_WithMoq()
		{
			// Arrange
			var mockDbContext = new Mock<WebShopDbContext>(MockBehavior.Strict, new DbContextOptions<WebShopDbContext>());
			mockDbContext.Setup(m => m.Dispose());

			var unitOfWork = new UnitOfWork(
				mockDbContext.Object,
				A.Fake<ProductSubject>(),
				A.Fake<IProductRepository>(),
				A.Fake<IOrderRepository>(),
				A.Fake<ICustomerRepository>()
			);

			// Act
			unitOfWork.Dispose();

			// Assert
			mockDbContext.Verify(m => m.Dispose(), Times.Once);
		}
	}
}
