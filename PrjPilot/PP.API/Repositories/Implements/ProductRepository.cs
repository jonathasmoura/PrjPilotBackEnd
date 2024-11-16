using Microsoft.EntityFrameworkCore;
using PP.API.DataContexts;
using PP.API.DTOs;
using PP.API.Models;
using PP.API.Repositories.Interfaces;
using System;

namespace PP.API.Repositories.Implements
{
	public class ProductRepository : IProductRepository
	{
		private readonly PilotContext _pilotContext;

		public ProductRepository(PilotContext pilotContext)
		{
			_pilotContext = pilotContext;
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			var allProducts = await _pilotContext.Products.ToListAsync();

			return allProducts;
		}

		public async Task<Product> GetProduct(int productId)
		{
			return await _pilotContext.Products
				.FirstOrDefaultAsync(x => x.Id == productId);
		}

		public async Task<Product> AddProduct(ProductDto productDto)
		{
			var obj = new Product
			{
				Id = productDto.Id,
				Name = productDto.Name,
				Description = productDto.Description,
				Price = productDto.Price,
				Quantity = productDto.Quantity
			};
			var result = await _pilotContext.Products.AddAsync(obj);
			await _pilotContext.SaveChangesAsync();

			return result.Entity;
		}

		public async Task<Product> UpdateProduct(ProductDto productDto)
		{

			var productToEdit = await _pilotContext.Products.FirstOrDefaultAsync(x => x.Id == productDto.Id);
			if (productToEdit != null)
			{


				productToEdit.Name = productDto.Name;
				productToEdit.Description = productDto.Description;
				productToEdit.Price = productDto.Price;
				productToEdit.Quantity = productDto.Quantity;

				_pilotContext.Entry(productToEdit).State = EntityState.Modified;
				await _pilotContext.SaveChangesAsync();

				return productToEdit;
			}

			return null;
		}

		public async Task DeleteProduct(int productId)
		{
			var result = await _pilotContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

			if (result != null)
			{
				_pilotContext.Entry(result).State = EntityState.Deleted;
				await _pilotContext.SaveChangesAsync();
			}
		}
	}
}
