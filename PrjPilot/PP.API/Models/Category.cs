using PP.API.Models.ModelBase;

namespace PP.API.Models
{
    public class Category : EntityBase
	{
       
        public string Name { get; set; }
        public string Description{ get; set; }
        //public string IdProduct { get; set; }
        //public virtual List<Product> Product { get; set; }
    }
}
