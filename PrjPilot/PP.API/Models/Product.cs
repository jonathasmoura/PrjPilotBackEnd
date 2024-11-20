﻿using PP.API.Models.ModelBase;

namespace PP.API.Models
{
	public class Product : EntityBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		//public int CategoryId { get; set; }
		//public virtual Category Category { get; set; }

	}
}
