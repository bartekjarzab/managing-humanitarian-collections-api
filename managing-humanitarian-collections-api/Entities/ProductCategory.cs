using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
