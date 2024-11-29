using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Entities;
using WebShop.Core.Interfaces.Product;

namespace WebShop.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;//

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			try
			{
				var allProducts = await _productService.GetAll();
				if (allProducts == null || !allProducts.Any())
				{
					return NotFound("No products found");
				}
				return Ok(allProducts);
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Internal server error {e.Message}");
			}
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id)
		{
			try
			{
				var product = await _productService.GetById(id);

				if (product == null)
				{
					return NotFound("No products found.");
				}
				return Ok(product);
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Something went wrong: {e.Message}");
			}
		}

		[HttpGet("name/{name}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
		{
			try
			{
				var products = await _productService.GetProductByName(name);

				if (products == null || !products.Any())
				{
					return NotFound("No products found.");
				}
				return Ok(products);
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Something went wrong: {e.Message}");
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddProduct(Product product)
		{
			
			if (product == null)
				return BadRequest("Product is null");
			try
			{
				await _productService.AddProduct(product);
				
				return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Internal PRODUCT server error: {e.Message}");
			}
		}

		[HttpPut]
		public async Task<ActionResult> UpdateProduct(Product product)
		{
			if (product == null)
				return BadRequest("Invalid data was submitted");

			try
			{
				await _productService.Update(product);


				return Ok($"Product {product.Name} was updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Internal server Error {e.Message}");
			}


		}

		[HttpDelete]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			if (id == null)
				return BadRequest("Invalid id submitted");

			try
			{
				await _productService.Delete(id);
				return Ok($"Product with {id} was deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Internal server error {e.Message}");
			}

		}
	}
}
