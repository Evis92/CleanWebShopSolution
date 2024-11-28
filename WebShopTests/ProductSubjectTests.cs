using FakeItEasy;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces;
using WebShop.Notifications;

namespace WebShopTests;

public class ProductSubjectTests
{
	[Fact]
	public async Task Attach_AddsObserverToList_CallToUpdate_HappensOnce()
	{
		//Arrange
		var productSubject = new ProductSubject();
		var observer = A.Fake<INotificationObserver>();

		// Act
		productSubject.Attach(observer);

		//Assert
		var fakeProduct = A.Fake<Product>();
		productSubject.Notify(fakeProduct);

		A.CallTo(() => observer.Update(fakeProduct)).MustHaveHappenedOnceExactly();
	}


	[Fact]
	public async Task Detach_RemovesObserverFromList_CallToUpdate_DoesNotHappen()
	{
		// Arrange
		var productSubject = new ProductSubject();
		var fakeObserver = A.Fake<INotificationObserver>();

		productSubject.Attach(fakeObserver);

		// Act
		productSubject.Detach(fakeObserver);

		// Assert
		var fakeProduct = A.Fake<Product>();
		productSubject.Notify(fakeProduct);

		A.CallTo(() => fakeObserver.Update(fakeProduct)).MustNotHaveHappened();
	}


	[Fact]
	public async Task Notify_CallsUpdateOnAllObservers()
	{
		// Arrange
		var productSubject = new ProductSubject();
		var fakeObserver1 = A.Fake<INotificationObserver>();
		var fakeObserver2 = A.Fake<INotificationObserver>();
		productSubject.Attach(fakeObserver1);
		productSubject.Attach(fakeObserver2);
		var fakeProduct = A.Fake<Product>();

		// Act
		productSubject.Notify(fakeProduct);

		// Assert
		A.CallTo(() => fakeObserver1.Update(fakeProduct)).MustHaveHappenedOnceExactly();
		A.CallTo(() => fakeObserver2.Update(fakeProduct)).MustHaveHappenedOnceExactly();
	}
}