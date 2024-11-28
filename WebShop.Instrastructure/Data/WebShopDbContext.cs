using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;

namespace WebShop.Instrastructure.Data;

public class WebShopDbContext : DbContext
{
	public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
	{
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Order> Orders { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.Entity<Order>()
		//	.HasMany()

		//modelBuilder.Entity<Customer>()
		//	.HasMany(c => c.Orders)
		//	.WithOne()
		//	.HasForeignKey(o => o.CustomerId)
		//	.IsRequired();

		base.OnModelCreating(modelBuilder);
	}
}