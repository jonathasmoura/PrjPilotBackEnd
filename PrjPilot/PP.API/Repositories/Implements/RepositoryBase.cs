using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using PP.API.Models.ModelBase;
using PP.API.Repositories.Interfaces;

namespace PP.API.Repositories.Implements
{
	public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T : EntityBase
	{

		protected readonly DbContext _context;
		protected DbSet<T> dbSet;
		private readonly IUnitOfWork _unitOfWork;

		public RepositoryBase(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			dbSet = _unitOfWork.context.Set<T>();
		}
		public async Task<ActionResult<IEnumerable<T>>> Get()
		{
			var data = await dbSet.ToListAsync();
			return Ok(data);
		}
		public async Task<T> GetById(int id)
		{
			var data = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
			return data;
		}
		public async Task<ActionResult<T>> Create(T entity)
		{
			
			dbSet.Add(entity);
			await _unitOfWork.SaveChangesAsync();
			return entity;
		}

		public async Task<IActionResult> Update(int id, T entity)
		{
			if (id != entity.Id)
			{
				return BadRequest();
			}

			var objToUpdate = await dbSet.FindAsync(id);
			if (objToUpdate == null)
			{
				return NotFound();
			}
			objToUpdate.UpdatedDate = DateTime.Now;

			_unitOfWork.context.Entry(objToUpdate).CurrentValues.SetValues(objToUpdate);

			try
			{
				await _unitOfWork.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{

				throw;
			}
			return NoContent();
		}

		public async Task<IActionResult> Delete(int id)
		{
			var data = await dbSet.FindAsync(id);

			if (data == null)
			{
				return NotFound();
			}
			dbSet.Remove(data);
			await _unitOfWork.SaveChangesAsync();
			return NoContent();
		}
	}
}
