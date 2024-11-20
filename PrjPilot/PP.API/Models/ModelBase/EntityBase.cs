
using Microsoft.JSInterop;

namespace PP.API.Models.ModelBase
{
	public class EntityBase
	{
		public EntityBase()
		{
			CreatedDate =  DateTime.Now;
		}
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		
	}
}
