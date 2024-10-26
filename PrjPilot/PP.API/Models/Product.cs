namespace PP.API.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
        //public int CategoryId { get; set; }
        //public virtual Category Category { get; set; }

        //	id:string;
        //name: string;
        //description:string;
        //price: number;
        //quantity: number;
    }
}
