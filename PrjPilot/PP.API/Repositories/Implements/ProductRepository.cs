using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Interfaces;
using System;

namespace PP.API.Repositories.Implements
{
	public class ProductRepository : RepositoryBase<Product>, IProductRepository
	{
		public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<IEnumerable<ProductDto>> GetAllAsync()
		{
			throw new NotImplementedException();
		}
	}
}
