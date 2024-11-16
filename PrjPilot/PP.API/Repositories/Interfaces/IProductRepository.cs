using PP.API.DTOs;
using PP.API.Models;

namespace PP.API.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAllProducts();
		Task<Product> GetProduct(int product); Task
		<Product> AddProduct(ProductDto product);
		Task<Product> UpdateProduct(ProductDto product);
		 Task DeleteProduct(int id);
	}
}
