using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.Repositories.Interfaces;
using System;

namespace PP.API.Repositories.Implements
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly PilotContext _context;
		private bool _disposed = false;

		public UnitOfWork(PilotContext context)
		{
			_context = context;
		}
		public DbContext context => _context;
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				_disposed = true;
			}
		}

	}
}
