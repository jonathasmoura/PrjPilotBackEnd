using Microsoft.EntityFrameworkCore;

namespace PP.API.Repositories.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		DbContext context { get; }
		public Task SaveChangesAsync();
	}
}
