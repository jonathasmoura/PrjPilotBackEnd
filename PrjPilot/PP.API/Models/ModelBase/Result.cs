using PP.API.Models.ModelBase;
using PP.API.Models;
using System.Security.Principal;

namespace PP.API.Models.ModelBase
{
	public class Result<T> where T : EntityBase
	{
		public int StatusCode { get; set; }
		public int Count { get; set; }
		public IEnumerable<T> Data { get; set; }
	}
}


//var list = _userRepository.GetAll();
//var model = new Result<User>
//{
//	StatusCode = 200,
//	Count = list.Count,
//	Data = list
//};

//return Ok(model);
