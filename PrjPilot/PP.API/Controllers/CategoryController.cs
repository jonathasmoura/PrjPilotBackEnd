using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Implements;
using PP.API.Repositories.Interfaces;

namespace PP.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		IRepository<Category> _categoryRepository;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_categoryRepository = new CategoryRepository(_unitOfWork);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Route("categories")]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
		{
			try
			{
				var all = await _categoryRepository.Get();
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
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Route("getby/{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			try
			{
				var result = await _categoryRepository.GetById(id);

				if (result == null)
				{
					return NotFound(new { message = $"Não foi possível localizar categoria com o código {id}." });
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
		[Route("addcategory")]
		public async Task<ActionResult<Category>> IncludeCategory([FromBody] Category category)
		{

			try
			{
				if (category == null)
				{
					return BadRequest();
				}

				var categoryCreated = await _categoryRepository.Create(category);

				return categoryCreated;
			}
			catch (Exception ex)
			{

				return StatusCode(StatusCodes.Status500InternalServerError,
				"Erro ao criar novo registro de categoria" + ex);
			}
		}

		[HttpPut]
		[Route("edit/{id}")]
		public async Task<IActionResult> UpdateCategory(int id, Category category)
		{
			try
			{
				if (id != category.Id)
					return BadRequest("Código de categorias, não são compatíveis!");

				var categoryToUpdate = await _categoryRepository.GetById(id);

				if (categoryToUpdate == null)
					return NotFound($"Não foi possível localizar categoria de código = {id} em nossa base de dados.");

				return await _categoryRepository.Update(id,category);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar registro!" + ex);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<CategoryDto>> DeleteCategory(int id)
		{
			try
			{
				var categoryToDelete = await _categoryRepository.GetById(id);
				if (categoryToDelete == null)
				{
					return NotFound($"Categoria com Id = {id} não encontrado");
				}

				var objToDelet = await _categoryRepository.Delete(id);
				return Ok("Categoria Excluído com Sucesso");
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir dados");
			}

		}
	}
}
