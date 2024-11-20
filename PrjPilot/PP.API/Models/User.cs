using PP.API.Models.ModelBase;

namespace PP.API.Models
{
	public class User : EntityBase
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int Telephone { get; set; }
	}
}
