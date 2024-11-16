using PP.API.DTOs;
using PP.API.Models;

namespace PP.API.Repositories.Interfaces
{
	public interface ICategoryRepository

	{
		Task<IEnumerable<Category>> GetAllCategories();
		Task<Category> GetCategory(int categoryId); Task
		<Category> AddCategory(CategoryDto category);
		Task<Category> UpdateCategory(CategoryDto category);
		Task DeleteCategory(int categoryId);
	}
}
