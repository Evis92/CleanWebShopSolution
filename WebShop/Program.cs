using Microsoft.EntityFrameworkCore;
using WebShop.Application.Payment;
using WebShop.Application.Services;
using WebShop.Core.Interfaces;
using WebShop.Core.Interfaces.Customer;
using WebShop.Core.Interfaces.Order;
using WebShop.Core.Interfaces.Product;
using WebShop.Instrastructure.Data;
using WebShop.Instrastructure.Repositories;
using WebShop.Instrastructure.UnitOfWork;
using WebShop.Notifications;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<WebShopDbContext>(options =>
	options.UseInMemoryDatabase("MyDatabase"));

// Registrera Unit of Work i DI-container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//builder.Services.AddSingleton<ProductSubject>();
//builder.Services.AddTransient<INotificationObserver, EmailNotification>();

builder.Services.AddSingleton<ProductSubject>(sp =>
{
	var productSubject = new ProductSubject();
	productSubject.Attach(new EmailNotification());
	productSubject.Attach(new SmsNotification());
	return productSubject;
});

builder.Services.AddTransient<CheckoutService>();
builder.Services.AddTransient<CreditCardPayment>();
builder.Services.AddTransient<SwishPayment>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
