using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Interfaces;

namespace PP.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		[Route("categories")]
		public async Task<IActionResult> GetAllProducts()
		{
			try
			{
				var all = await _categoryRepository.GetAllCategories();
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
		public async Task<IActionResult> GetCategoryById(int id)
		{
			try
			{
				var result = await _categoryRepository.GetCategory(id);

				if (result == null)
				{
					return NotFound(new { message = $"N�o foi poss�vel localizar categoria com o c�digo {id}." });
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
		public async Task<IActionResult> IncludeCategory([FromBody] CategoryDto categoryDto)
		{

			try
			{
				if (categoryDto == null)
				{
					return BadRequest();
				}

				var categoryCreated = await _categoryRepository.AddCategory(categoryDto);

				return CreatedAtAction(nameof(GetCategoryById), new { id = categoryCreated.Id }, categoryCreated);
			}
			catch (Exception ex)
			{

				return StatusCode(StatusCodes.Status500InternalServerError,
				"Erro ao criar novo registro de categoria" + ex);
			}
		}

		[HttpPut]
		[Route("edit/{id}")]
		public async Task<ActionResult<Category>> UpdateCategory(int id, CategoryDto categoryDto)
		{
			try
			{
				if (id != categoryDto.Id)
					return BadRequest("C�digo de categorias, n�o s�o compat�veis!");

				var categoryToUpdate = await _categoryRepository.GetCategory(id);

				if (categoryToUpdate == null)
					return NotFound($"N�o foi poss�vel localizar categoria de c�digo = {id} em nossa base de dados.");

				return await _categoryRepository.UpdateCategory(categoryDto);

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
				var categoryToDelete = await _categoryRepository.GetCategory(id);
				if (categoryToDelete == null)
				{
					return NotFound($"Categoria com Id = {id} n�o encontrado");
				}

				await _categoryRepository.DeleteCategory(id);
				return Ok("Categoria Exclu�do com Sucesso");
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir dados");
			}

		}
	}
}
