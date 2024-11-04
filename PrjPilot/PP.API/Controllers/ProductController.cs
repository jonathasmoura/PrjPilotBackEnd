using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;

namespace PP.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly PilotContext _pilotContext;

		public ProductController(PilotContext pilotContext)
		{
			_pilotContext = pilotContext;
		}

		[HttpGet]
		[Route("products")]
		public async Task<IActionResult> GetAllProducts()
		{


			var allProducts = await _pilotContext.Products.ToListAsync();

			if (allProducts.Count <= 0)
			{
				return NotFound();
			}

			return Ok(allProducts);
		}

		[HttpGet]
		[Route("getby/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				var product = await _pilotContext.Products.FirstOrDefaultAsync(x => x.Id == id);
				if (product == null)
				{
					return NotFound(new { message = $"Não foi possível localizar produto com o código {id}." });
				}
				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error" + ex);
			}
		}

		[HttpPost]
		[Route("addproduct")]
		public async Task<IActionResult> IncludeCategory([FromBody] ProductDto productDto)
		{
			if (productDto == null)
			{
				return BadRequest();
			}

			var obj = new Product
			{
				Id = productDto.Id,
				Name = productDto.Name,
				Description = productDto.Description,
				Price = productDto.Price,
				Quantity = productDto.Quantity
			};

			await _pilotContext.Products.AddAsync(obj);
			await _pilotContext.SaveChangesAsync();

			return Ok(obj);
		}

		[HttpPut]
		[Route("edit/{id}")]
		public async Task<ActionResult<ProductDto>> UpdateProduct(int id, ProductDto productDto)
		{
			try
			{
				if (id != productDto.Id)
					return BadRequest("Código de produtos, não são compatíveis!");

				var productToEdit = await _pilotContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
				if (productToEdit == null)
				{
					return NotFound($"Não foi possível localizar produto de código = {id} em nossa base de dados.");

				}

				productToEdit.Name = productDto.Name;
				productToEdit.Description = productDto.Description;
				productToEdit.Price = productDto.Price;
				productToEdit.Quantity = productDto.Quantity;

				_pilotContext.Entry(productToEdit).State = EntityState.Modified;
				await _pilotContext.SaveChangesAsync();

				return productDto;
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar registro!");
			}
		}
	}
}
