using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Interfaces;

namespace PP.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		[Route("products")]
		public async Task<IActionResult> GetAllProducts()
		{
			try
			{
				var all = await _productRepository.GetAllProducts();
				if (all.Count() <= 0)
				{
					return NotFound();
				}
				return Ok(all);
			}
			catch (Exception ex)
			{

				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Erro ao recuperar dados do banco de dados" + ex.Message);
			}
		}

		[HttpGet]
		[Route("getby/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				var result = await _productRepository.GetProduct(id);

				if (result == null)
				{
					return NotFound(new { message = $"N�o foi poss�vel localizar produto com o c�digo {id}." });
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Erro ao recuperar dados do banco de dados" + ex);
			}
		}

		[HttpPost]
		[Route("addproduct")]
		public async Task<IActionResult> IncludeProduct([FromBody] ProductDto productDto)
		{

			try
			{
				if (productDto == null)
				{
					return BadRequest();
				}

				var productCreated = await _productRepository.AddProduct(productDto);

				return CreatedAtAction(nameof(GetProductById), new { id = productCreated.Id }, productCreated);
			}
			catch (Exception ex)
			{

				return StatusCode(StatusCodes.Status500InternalServerError,
				"Erro ao criar novo registro de produto" + ex);
			}
		}

		[HttpPut]
		[Route("edit/{id}")]
		public async Task<ActionResult<Product>> UpdateProduct(int id, ProductDto productDto)
		{
			try
			{
				if (id != productDto.Id)
					return BadRequest("C�digo de produtos, n�o s�o compat�veis!");

				var productToUpdate = await _productRepository.GetProduct(id);

				if (productToUpdate == null)
					return NotFound($"N�o foi poss�vel localizar produto de c�digo = {id} em nossa base de dados.");

				return await _productRepository.UpdateProduct(productDto);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar registro!" + ex);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
		{
			try
			{
				var productToDelete = await _productRepository.GetProduct(id);
				if(productToDelete == null)
				{
					return NotFound($"Produto com Id = {id} n�o encontrado");
				}

				await _productRepository.DeleteProduct(id);
				return Ok("Produto Exclu�do com Sucesso");
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir dados");
			}


		}
	}
}
