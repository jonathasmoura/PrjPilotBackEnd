using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Interfaces;

namespace PP.API.Repositories.Implements
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly PilotContext _pilotContext;

		public CategoryRepository(PilotContext pilotContext)
		{
			_pilotContext = pilotContext;
		}

		public async Task<Category> AddCategory(CategoryDto categoryDto)
		{
			var obj = new Category
			{
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				Description = categoryDto.Description
			};
			var result = await _pilotContext.Categories.AddAsync(obj);
			await _pilotContext.SaveChangesAsync();

			return result.Entity;
		}


		public async Task<IEnumerable<Category>> GetAllCategories()
		{
			var allCategories = await _pilotContext.Categories.ToListAsync();

			return allCategories;
		}

		public async Task<Category> GetCategory(int categoryId)
		{
			return await _pilotContext.Categories
				.FirstOrDefaultAsync(x => x.Id == categoryId);
		}

		public async Task<Category> UpdateCategory(CategoryDto categoryDto)
		{

			var categoryToEdit = await _pilotContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryDto.Id);
			if (categoryToEdit != null)
			{


				categoryToEdit.Name = categoryDto.Name;
				categoryToEdit.Description = categoryDto.Description;
			

				_pilotContext.Entry(categoryToEdit).State = EntityState.Modified;
				await _pilotContext.SaveChangesAsync();

				return categoryToEdit;
			}

			return null;
		}
		public async Task DeleteCategory(int categoryId)
		{
			var result = await _pilotContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

			if (result != null)
			{
				_pilotContext.Entry(result).State = EntityState.Deleted;
				await _pilotContext.SaveChangesAsync();
			}
		}

	}
}
