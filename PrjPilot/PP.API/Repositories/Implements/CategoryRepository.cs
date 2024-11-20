using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Interfaces;

namespace PP.API.Repositories.Implements
{
	public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
	{
		public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<Category> AddCategory(CategoryDto category)
		{
			throw new NotImplementedException();
		}

		public Task DeleteCategory(int categoryId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Category>> GetAllCategories()
		{
			throw new NotImplementedException();
		}

		public Task<Category> GetCategory(int categoryId)
		{
			throw new NotImplementedException();
		}

		public Task<Category> UpdateCategory(CategoryDto category)
		{
			throw new NotImplementedException();
		}
	}
}
