namespace WebShop.Core.Entities;

public class Product
{
	public int Id { get; set; } // Unikt ID för produkten
	public string Name { get; set; } // Namn på produkten
	public double Price { get; set; }
}