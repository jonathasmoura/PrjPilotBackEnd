using Microsoft.AspNetCore.Mvc;
using PP.API.Models.ModelBase;

namespace PP.API.Repositories.Interfaces
{
	public interface IRepository<T> where T :  EntityBase
	{
		public Task<ActionResult<IEnumerable<T>>> Get();
		public Task<T> GetById(int id);
		public Task<ActionResult<T>> Create(T entity);
		public Task<IActionResult> Update(int id, T entity);
		public Task<IActionResult> Delete(int id);
	}
}
