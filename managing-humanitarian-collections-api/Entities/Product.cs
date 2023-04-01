using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductProperties Properties { get; set; }

        public virtual ProductCategory Category { get; set; }
    }
}
