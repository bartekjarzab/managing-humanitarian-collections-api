using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

    //    public int ProductPropertiesId { get; set; }
        public int ProductCategoryId { get; set; }

    //    public virtual ProductProperties ProductProperties { get; set; }
        public  ProductCategory ProductCategory { get; set; }

        public virtual ProductProperties Properties { get; set; }
    }
}
