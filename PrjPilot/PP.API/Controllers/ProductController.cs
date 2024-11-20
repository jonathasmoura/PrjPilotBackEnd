using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Models.ModelBase;
using PP.API.Repositories.Implements;
using PP.API.Repositories.Interfaces;

namespace PP.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		IRepository<Product> _productRepository;

		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_productRepository = new ProductRepository(_unitOfWork);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Route("products")]
		public async Task<ActionResult<IEnumerable<Result<Product>>>> GetAllProducts()
		{
			try
			{

				var all = await _productRepository.Get();
				if (all == null)
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Route("getby/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				var result = await _productRepository.GetById(id);

				if (result == null)
				{
					return NotFound(new { message = $"Não foi possível localizar produto com o código {id}." });
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Product>> IncludeProduct([FromBody] Product product)
		{

			try
			{

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (product == null)
				{
					return BadRequest();
				}

				var productCreated = await _productRepository.Create(product);

				return productCreated;
			}
			catch (Exception ex)
			{

				return StatusCode(StatusCodes.Status500InternalServerError,
				"Erro ao criar novo registro de produto" + ex);
			}
		}

		[HttpPut]
		[Route("edit/{id}")]
		public async Task<IActionResult> UpdateProduct(int id, Product product)
		{
			try
			{
				if (id != product.Id)
					return BadRequest("Código de produtos, não são compatíveis!");

				var productToUpdate = await _productRepository.GetById(id);

				if (productToUpdate == null)
					return NotFound($"Não foi possível localizar produto de código = {id} em nossa base de dados.");

				var objProduct = await _productRepository.Update(id, product);
				return objProduct;

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar registro!" + ex);
			}
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
		{
			try
			{
				var productToDelete = await _productRepository.GetById(id);
				if (productToDelete == null)
				{
					return NotFound($"Produto com Id = {id} não encontrado");
				}

				var objToDelete = await _productRepository.Delete(id);
				return Ok("Produto Excluído com Sucesso");
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir dados");
			}


		}
	}
}
