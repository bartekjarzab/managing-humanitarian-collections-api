using System.Collections.Generic;

namespace managing_humanitarian_collections_api.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int ProductPropertiesId { get; set; }
        public int ProductCategoryId { get; set; }
      
       // public List<ProductPropertiesDto> ProductProperties { get; set; }
    }
}
