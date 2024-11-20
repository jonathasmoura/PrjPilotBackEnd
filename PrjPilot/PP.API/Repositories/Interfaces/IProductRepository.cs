using PP.API.DTOs;
using PP.API.Models;

namespace PP.API.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductDto>> GetAllAsync();
	}
}
