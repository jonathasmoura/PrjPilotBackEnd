using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;

namespace PP.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly PilotContext _pilotContext;

		public CategoryController(PilotContext pilotContext)
		{
			_pilotContext = pilotContext;
		}

		[HttpGet]
		[Route("categories")]
		public async Task<IActionResult> GetAllCategories()
		{


			var allCategories = await _pilotContext.Categories.ToListAsync();

			if (allCategories.Count <= 0)
			{
				return NotFound();
			}

			return Ok(allCategories);
		}

		[HttpPost]
		[Route("addcategory")]
		public async Task<IActionResult> IncludeCategory([FromBody] CategoryDto categoryDto)
		{
			if (categoryDto == null)
			{
				return BadRequest();
			}

			var obj = new Category
			{
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				Description = categoryDto.Description
			};

			await _pilotContext.Categories.AddAsync(obj);
			await _pilotContext.SaveChangesAsync();

			return Ok(obj);
		}



	}
}
